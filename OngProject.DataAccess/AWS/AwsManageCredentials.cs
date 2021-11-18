using Amazon;

namespace OngProject.DataAccess.AWS
{
    public class AwsManageCredentials
    {
        public string Bucket {get; set;}
        public string AccessKeyId {get; set;}
        public string SecretAccessKey {get; set;}
        public RegionEndpoint Region => RegionEndpoint.USEast1;
    }
}