﻿using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace LibHouse.API.Configurations.Secrets
{
    public class AmazonSecretsManagerConfigurationProvider : ConfigurationProvider
    {
        private readonly string _region;
        private readonly string _secretName;

        public AmazonSecretsManagerConfigurationProvider(string region, string secretName)
        {
            _region = region;
            _secretName = secretName;
        }

        public override void Load()
        {
            var secret = GetSecret();
            Data = JsonSerializer.Deserialize<Dictionary<string, string>>(secret);
        }

        private string GetSecret()
        {
            var request = new GetSecretValueRequest
            {
                SecretId = _secretName,
                VersionStage = "AWSCURRENT" 
            };
            using var client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(_region));
            var response = client.GetSecretValueAsync(request).Result;
            string secretString;
            if (response.SecretString != null)
            {
                secretString = response.SecretString;
            }
            else
            {
                var memoryStream = response.SecretBinary;
                var reader = new StreamReader(memoryStream);
                secretString = Encoding.UTF8.GetString(Convert.FromBase64String(reader.ReadToEnd()));
            }
            return secretString;
        }
    }
}