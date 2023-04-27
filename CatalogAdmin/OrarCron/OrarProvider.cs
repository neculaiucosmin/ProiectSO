using System.Net;
using System.Text;
using CatalogBackend.Entities;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace OrarCron;

public class OrarProvider
{
    public async Task <List<string>> GoToUPG(int sem)
    {
        var options = new ChromeOptions();
        options.AddArguments("start-maximized");
        var driver = new ChromeDriver(options);
        driver.Navigate().GoToUrl("http://ac.upg-ploiesti.ro/orar.php");
        var listOfLinks = driver.FindElements(By.CssSelector("li>a")).Select(s => s.GetAttribute("href")).ToList();
        Thread.Sleep(300);
        var listOfOrareSem2 = new List<string>();

        foreach (var LINK in listOfLinks)
            if (LINK != null && LINK.Contains("sem" + sem))
                listOfOrareSem2.Add(LINK);

        driver.Close();

        return listOfOrareSem2;
    }

    public bool CompareFile(string localFilePath, byte[] file2)
    {
        bool areEqual;
        using (var fileStream = File.OpenRead(localFilePath))
        {
            var localFileBytes = new byte[fileStream.Length];

            fileStream.Read(localFileBytes, 0, localFileBytes.Length);

            areEqual = file2.SequenceEqual(localFileBytes);
        }

        if (areEqual)
            return true;
        return false;
    }

    public async Task StartCron()
    {
        var isActionNeeded = false;

        var ListOfOrars = await GoToUPG(2);
        var client = new WebClient();
        var bytes = new List<byte[]>();
        foreach (var item in ListOfOrars)
            //client.DownloadFile(item,$@"C:\Users\cosmi\Desktop\Github\ProiectSO\CatalogAdmin\OrarCron\Orare\{item.Replace("http://ac.upg-ploiesti.ro/orar/sem2/","")}");
            bytes.Add(client.DownloadData(item));

        foreach (var fileName in Directory.GetFiles(
                     @"C:\Users\cosmi\Desktop\Github\ProiectSO\CatalogAdmin\OrarCron\Orare"))
            Console.WriteLine(fileName);

        var DirectoryPaths =
            Directory.GetFiles(@"C:\Users\cosmi\Desktop\Github\ProiectSO\CatalogAdmin\OrarCron\Orare").ToList();
        DirectoryPaths.Sort();
        var FilesThatNeedAttention = new StringBuilder("Urmatoarele orare trebuiesc modificate: ");
        for (var i = 0; i < bytes.Count; i++)
            if (!CompareFile(DirectoryPaths[i], bytes[i]))
            {
                isActionNeeded = true;
                FilesThatNeedAttention.Append(DirectoryPaths[i] + ",");
                Console.WriteLine(false);
            }

        if (isActionNeeded) await Request(FilesThatNeedAttention.ToString());
    }

    private async Task Request(string s)
    {
        var mailRequest = new MailRequest
        {
            ToEmail = "example@example.com",
            Subject = "Test Email",
            Body = s
        };
        try
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            var client = new HttpClient(handler);
            
            var content = new StringContent(string.Empty, Encoding.UTF8, "application/json");
            //var queryParams = $"?ToEmail={Uri.EscapeDataString(mailRequest.ToEmail)}&Subject={Uri.EscapeDataString(mailRequest.Subject)}&Body={Uri.EscapeDataString(mailRequest.Body)}";
            var response = await client.PostAsync($"https://localhost:7069/api/send_mail?ToEmail={mailRequest.ToEmail}%40mail.com&Subject={mailRequest.Subject}&Body={mailRequest.Body}%20",null);
            // var content = new StringContent(mailJson, Encoding.UTF8, "application/json");
            //
            // var response = await client.PostAsync("https://localhost:7069/api/send_mail", content);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Response status code: {response.StatusCode}");
                throw new HttpRequestException();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Ceva nu a functionat");
            throw;
        }
    }
}