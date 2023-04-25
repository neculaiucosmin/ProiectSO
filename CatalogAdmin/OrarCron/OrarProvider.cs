using System.Net;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace OrarCron;

public class OrarProvider
{
     public List<string> GoToUPG(int sem)
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
        int file1bite;
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

    public void StartCron()
    {
        var isActionNeeded = false;

        var ListOfOrars = GoToUPG(2);
        var client = new WebClient();
        var bytes = new List<byte[]>();
        foreach (var item in ListOfOrars)
        {
            //client.DownloadFile(item,$@"C:\Users\cosmi\Desktop\Github\ProiectSO\CatalogAdmin\OrarCron\Orare\{item.Replace("http://ac.upg-ploiesti.ro/orar/sem2/","")}");
            bytes.Add(client.DownloadData(item));
        }

        foreach (var fileName in Directory.GetFiles(@"C:\Users\cosmi\Desktop\Github\ProiectSO\CatalogAdmin\OrarCron\Orare"))
            Console.WriteLine(fileName);

        var DirectoryPaths =
            Directory.GetFiles(@"C:\Users\cosmi\Desktop\Github\ProiectSO\CatalogAdmin\OrarCron\Orare").ToList();
        DirectoryPaths.Sort();
        for (var i = 0; i < bytes.Count; i++)
        {
            if (CompareFile(DirectoryPaths[i], bytes[i]))
            {
                //TODO: Api request when false 
            }
        }
    }
}