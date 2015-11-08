using System.Net;

namespace MongoDBPrototype
{
    public class FileDownloader
    {

        /// <summary>
        ///     Download a file from a url.
        /// </summary>
        /// <param name="urlString">URL of file as a string</param>
        /// <param name="destinationFullName">Full path to where this file will go.</param>
        public static void DownloadFile(string urlString, string destinationFullName)
        {
            var webClient = new WebClient();
            webClient.DownloadFile(urlString, destinationFullName);
        }

    }
}