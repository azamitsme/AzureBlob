using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;

namespace storage.Common
{
    public class ManipulateBlob
    {

        public void uploadfile(BlobContainerClient blobContainerClient)
        {
            var path = "./notes/MyFirstBlob.txt";

            var blobClient = blobContainerClient.GetBlobClient("MyFirstBlob.txt");

            var fileBytes = File.ReadAllBytes(path);

            var ms = new MemoryStream(fileBytes);

            blobClient.Upload(ms, overwrite: true);

        }

        public void listfile(BlobContainerClient blobContainerClient){
            foreach(var blob in blobContainerClient.GetBlobs()){
                Console.WriteLine($"Blob {blob.Name} found!{blob.Properties}");
                if(blob.Name.Contains("MyFirstBlob.txt")){
                    Console.WriteLine($"Blob {blob.Name} exist!{blob.Properties}");
                }
            }
        }

        public void downloadfile(BlobContainerClient blobContainerClient){
            var blobClient = blobContainerClient.GetBlobClient("MyFirstBlob.txt");
            Console.WriteLine($"Blob {blobClient.Name} exists at {blobClient.Uri}");
            var downloadFileStream = new MemoryStream();
            blobClient.DownloadTo(downloadFileStream);
            var downloadFileBytes = downloadFileStream.ToArray();
            using(var f = File.Create($"{Environment.CurrentDirectory}/notes/MyFirstBlob-Download.txt")){
                f.Write(downloadFileBytes,0,downloadFileBytes.Length);
            }

        }

        public void modifyfile(BlobContainerClient blobContainerClient){
            //metadata
            foreach(var blob in blobContainerClient.GetBlobs()){
                Console.WriteLine($"{blob.Name} found! {blob.Properties}");
                if(blob.Name.Contains("MyFirstBlob.txt")){
                    blob.Metadata.Add("","");
                    blob.Metadata.Add("","");
                    blob.Metadata.Add("","");

                    var metadata = blob.Metadata;
                    foreach(var key in metadata.Keys){
                        Console.WriteLine($"Metadata {key} has value {metadata[key]}");
                    }
                }
            }
        }

        public void deletefile(BlobContainerClient blobContainerClient, string fileName){
            var blobClient = blobContainerClient.GetBlobClient(fileName);
            blobClient.DeleteIfExists();

        }

        public void deletefile(BlobContainerClient blobContainerClient){
            blobContainerClient.DeleteIfExists();
        }


    }

    
}