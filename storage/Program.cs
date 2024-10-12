using Azure.Storage.Blobs;

var StorageCtr = "";

var blobStorageClient = new BlobServiceClient(StorageCtr);

var exists = false;

var containers = blobStorageClient.GetBlobContainers().AsPages();

foreach(var containerPage in containers){
    foreach(var containersItem in containerPage.Values){
            if(containersItem.Name.Equals("notes")){
                exists = true;
                break;
            }
    }
    if(exists) break;
}
if(!exists){
    blobStorageClient.CreateBlobContainer("notes");
}

var containerClient = blobStorageClient.GetBlobContainerClient("notes");

