using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace XKCDDownloader
{
    class Downloader
    {

        /// <summary>
        /// Downloads the latest xkcd comic.
        /// </summary>
        public static void Download()
        {
            // The folder to download to (currently just the directory the .exe is stored in.
            string comicsFolder = AppDomain.CurrentDomain.BaseDirectory;

            WebClient HTMLclient = new WebClient();

            // The HTML of xkcd.com
            var HTMLdata = HTMLclient.DownloadData(Globals.site);
            var siteHTML = UTF8Encoding.ASCII.GetString(HTMLdata);

            HTMLclient.Dispose();

            WebClient fileDonwloadClient = new WebClient();

            // All the things needed to download and save the file.
            var title = getTitle(siteHTML);
            var downloadURL = getURL(siteHTML);
            var destinationFolder = comicsFolder + "\\XKCDComics";
            System.IO.Directory.CreateDirectory(destinationFolder); 
            var destinationFile = destinationFolder + "\\" + title + ".png";

            fileDonwloadClient.DownloadFile(downloadURL, destinationFile);

            fileDonwloadClient.Dispose();
            
        }

        /// <summary>
        /// Gets the URL of the comic to download.
        /// </summary>
        /// <param name="siteHTML"></param>
        /// <returns></returns>
        public static String getURL(String siteHTML)
        {
            var document = new HtmlDocument();
            document.LoadHtml(siteHTML);

            var cURL = document.GetElementbyId("comic").Element("img").GetAttributeValue("src", "No Source");

            return cURL;
        }

        /// <summary>
        /// Gets the title of the comic to Download.
        /// </summary>
        /// <param name="siteHTML"></param>
        /// <returns></returns>
        public static String getTitle(String siteHTML) 
        {
            var document = new HtmlDocument();
            document.LoadHtml(siteHTML);
            var ctitle = document.GetElementbyId("ctitle");

            return ctitle.InnerText;
        }
    }
}
