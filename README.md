# Sample Azure Integration Tests – README
To test integration b/w Jira and GitHub actions test reports
This repository contains sample Azure‑related integration tests written using xUnit.
These tests are lightweight, dependency‑free, and ideal for demonstrating how to run automated test suites in GitHub Actions, CI/CD pipelines, or local development environments.

📌 Overview
The test suite illustrates various testing scenarios including:

Basic validation tests
Email format verification
Resource naming conventions
Simple collection and object validation
Async operation testing
Error‑handling validation
JSON response parsing
Performance threshold testing

No Azure credentials are required — all Azure‑related behaviors are simulated for demonstration and learning purposes.

🧪 Test Categories
✅ 1. String & Input Validation
Demonstrates null/empty checks and basic validation logic.

Test_StringValidation_Success
Test_StringValidation_Failure


✅ 2. Email Format Validation
Uses System.Net.Mail.MailAddress to validate email addresses.

Valid emails are tested using [Theory] + [InlineData]
Invalid formats are tested similarly


✅ 3. Resource Name Generation
Tests a helper method that generates Azure‑style resource names in the format:
{prefix}-{environment}-{randomSuffix}


✅ 4. Async Operation Tests
Simulates Azure async operations with success/failure paths:

Successful async execution → returns "Success"
Failed execution → throws InvalidOperationException


✅ 5. Collection & Object Validation
Validates:

Lists and item counts
Simulated ResourceGroupModel with:

Name
Location
Tags dictionary




✅ 6. JSON Parsing Simulation
Verifies simple success keyword detection in API‑like JSON responses.

✅ 7. Performance Threshold Test
Ensures a loop operation completes within 1 second, demonstrating performance assertions.
