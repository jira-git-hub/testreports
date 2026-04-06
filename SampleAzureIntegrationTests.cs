using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourProject.Tests
{
    /// <summary>
    /// Sample Azure Integration Tests - No Credentials Required
    /// These tests demonstrate various testing scenarios that integrate with GitHub Actions
    /// </summary>
    public class SampleAzureIntegrationTests
    {
        [Fact]
        public void Test_StringValidation_Success()
        {
            // Arrange
            string input = "Hello Azure";
            
            // Act
            bool isValid = !string.IsNullOrWhiteSpace(input);
            
            // Assert
            Assert.True(isValid, "String should not be null or whitespace");
        }

        [Fact]
        public void Test_StringValidation_Failure()
        {
            // Arrange
            string? input = null;
            
            // Act
            bool isValid = !string.IsNullOrWhiteSpace(input);
            
            // Assert
            Assert.False(isValid, "Null string should fail validation");
        }

        [Theory]
        [InlineData("user@example.com")]
        [InlineData("admin@company.org")]
        [InlineData("test.user@domain.co.uk")]
        public void Test_EmailValidation_ValidFormats(string email)
        {
            // Act
            bool isValid = IsValidEmail(email);
            
            // Assert
            Assert.True(isValid, $"Email '{email}' should be valid");
        }

        [Theory]
        [InlineData("invalid-email")]
        [InlineData("@example.com")]
        [InlineData("user@")]
        public void Test_EmailValidation_InvalidFormats(string email)
        {
            // Act
            bool isValid = IsValidEmail(email);
            
            // Assert
            Assert.False(isValid, $"Email '{email}' should be invalid");
        }

        [Fact]
        public void Test_ResourceNameGeneration()
        {
            // Arrange
            string prefix = "storage";
            string environment = "prod";
            
            // Act
            string resourceName = GenerateResourceName(prefix, environment);
            
            // Assert
            Assert.NotNull(resourceName);
            Assert.StartsWith(prefix, resourceName);
            Assert.Contains(environment, resourceName);
        }

        [Fact]
        public async Task Test_AsyncOperation_Success()
        {
            // Arrange & Act
            var result = await SimulateAzureOperationAsync(true);
            
            // Assert
            Assert.NotNull(result);
            Assert.Equal("Success", result);
        }

        [Fact]
        public async Task Test_AsyncOperation_Failure()
        {
            // Arrange & Act
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                () => SimulateAzureOperationAsync(false)
            );
            
            // Assert
            Assert.NotNull(exception);
            Assert.Contains("Operation failed", exception.Message);
        }

        [Fact]
        public void Test_CollectionOperations()
        {
            // Arrange
            var resources = new List<string> { "storage1", "storage2", "storage3" };
            
            // Act
            int count = resources.Count;
            bool containsStorage1 = resources.Contains("storage1");
            
            // Assert
            Assert.Equal(3, count);
            Assert.True(containsStorage1);
        }

        [Fact]
        public void Test_ResourceGroupValidation()
        {
            // Arrange
            var resourceGroup = new ResourceGroupModel
            {
                Name = "my-resource-group",
                Location = "eastus",
                Tags = new Dictionary<string, string> { { "env", "prod" } }
            };
            
            // Act
            bool isValid = ValidateResourceGroup(resourceGroup);
            
            // Assert
            Assert.True(isValid);
            Assert.NotEmpty(resourceGroup.Tags);
        }

        [Fact]
        public void Test_ApiResponse_Parsing()
        {
            // Arrange
            string jsonResponse = "{\"status\":\"success\",\"data\":{\"id\":123,\"name\":\"test\"}}";
            
            // Act
            bool isParseable = !string.IsNullOrEmpty(jsonResponse) && jsonResponse.Contains("success");
            
            // Assert
            Assert.True(isParseable);
        }

        [Fact]
        public void Test_PerformanceThreshold()
        {
            // Arrange
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            
            // Act - Simulate some operation
            for (int i = 0; i < 10000; i++)
            {
                _ = Math.Sqrt(i);
            }
            stopwatch.Stop();
            
            // Assert - Should complete in less than 1 second
            Assert.True(stopwatch.ElapsedMilliseconds < 1000, 
                $"Operation took {stopwatch.ElapsedMilliseconds}ms, expected less than 1000ms");
        }

        // Helper Methods
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private string GenerateResourceName(string prefix, string environment)
        {
            return $"{prefix}-{environment}-{Guid.NewGuid().ToString().Substring(0, 8)}";
        }

        private async Task<string> SimulateAzureOperationAsync(bool shouldSucceed)
        {
            await Task.Delay(100); // Simulate async work
            
            if (shouldSucceed)
            {
                return "Success";
            }
            
            throw new InvalidOperationException("Operation failed");
        }

        private bool ValidateResourceGroup(ResourceGroupModel resourceGroup)
        {
            return !string.IsNullOrEmpty(resourceGroup.Name) &&
                   !string.IsNullOrEmpty(resourceGroup.Location) &&
                   resourceGroup.Tags != null;
        }
    }

    // Model Classes
    public class ResourceGroupModel
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public Dictionary<string, string> Tags { get; set; } = new();
    }
}
