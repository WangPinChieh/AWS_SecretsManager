using System.Text.Json;
using Amazon;
using Amazon.Runtime.CredentialManagement;
using AWS_ECS_CoreApi.Controllers;

namespace AWS_ECS_CoreApi;

public static class ServiceExtensions
{
    public static async Task AddAWSAccessCredentials(this IServiceCollection services)
    {
        var secret = await AWSSecretManager.GetSecret();
        var accessCredential = JsonSerializer.Deserialize<AccessCredential>(secret);

        var credentialProfileOptions = new CredentialProfileOptions
            {AccessKey = accessCredential.AccessKey, SecretKey = accessCredential.AccessSecret};
        var credentialProfile = new CredentialProfile("aws_profile", credentialProfileOptions)
        {
            Region = RegionEndpoint.GetBySystemName(accessCredential.Region)
        };
        new SharedCredentialsFile().RegisterProfile(credentialProfile);
    }
}