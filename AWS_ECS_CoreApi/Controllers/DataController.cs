using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Mvc;

namespace AWS_ECS_CoreApi.Controllers;

[ApiController]
[Route("Data")]
public class DataController : ControllerBase
{
    private readonly IAmazonS3 _amazonS3;
    private static readonly Random Random = new Random();

    public DataController(IAmazonS3 amazonS3)
    {
        _amazonS3 = amazonS3;
    }

    // GET
    [HttpGet]
    public async Task<IActionResult> GetData()
    {
        try
        {
            // var secret = await AWSSecretManager.GetSecret();
            // var accessCredential = JsonSerializer.Deserialize<AccessCredential>(secret);
            //
            // var credentialProfileOptions = new CredentialProfileOptions
            //     {AccessKey = accessCredential.AccessKey, SecretKey = accessCredential.AccessSecret};
            // var credentialProfile = new CredentialProfile("default", credentialProfileOptions)
            // {
            //     Region = RegionEndpoint.GetBySystemName(accessCredential.Region)
            // };
            // new SharedCredentialsFile().RegisterProfile(credentialProfile);

            // var listObjectsResponse = await _amazonS3.ListObjectsAsync("svtp-webportal-dev",
            //     "SVTP800/Opel13/DB/AC02",
            //     CancellationToken.None
            // );
            var listObjectsResponse = await _amazonS3.ListObjectsV2Async(new ListObjectsV2Request
            {
                BucketName = "svtp-webportal-dev",
                Prefix = "SVTP800/Opel13/DB/AC02",
            });
            return Ok(new {listObjectsResponse});
        }
        catch (Exception e)
        {
            return Content(e.ToString());
        }
    }

    [HttpGet("getIp")]
    public IActionResult GetIp()
    {
        var hostName = Dns.GetHostName();
        var myIP = Dns.GetHostByName(hostName).AddressList.Select(m => m.MapToIPv4().ToString());
        return Ok(new {IP = myIP});
    }

    [HttpPost("postData")]
    public async Task<IActionResult> PostData(string url)
    {
        using var client = new HttpClient();
        var response = await client.PostAsync(url, new StringContent(""));

        return Ok(response);
    }

    [HttpPost("receiveData")]
    public async Task<IActionResult> Receive()
    {
        return Ok(new {ReceivedDate = DateTime.Now});
    }
}