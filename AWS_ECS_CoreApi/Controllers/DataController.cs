using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime.CredentialManagement;
using Amazon.S3;
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

            var listObjectsResponse = await _amazonS3.ListObjectsAsync("svtp-webportal-dev",
                "svtp-webportal-dev/SVTP800/Opel13/DB/AC02/", CancellationToken.None);
            return Ok(new {listObjectsResponse});
        }
        catch (Exception e)
        {
            return Content(e.ToString());
        }
    }
}