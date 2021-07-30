[![Build Status](https://vitessepsp.visualstudio.com/MerchantAPI/_apis/build/status/SystemTests?branchName=master)](https://vitessepsp.visualstudio.com/MerchantAPI/_build/latest?definitionId=20&branchName=master)

## Synopsis

This solution contains the system tests for the Vitesse PSP Ltd system. These tests are intended to be black box tests that execute user interaction with the public facing components of the system. 

These are intended to be longer running tests that execute critical journies through the system.

## Key Information

- Web tests are executed using the Selenium wrapper called [Atata](https://atata.io/).
- API tests are executed the VitesseSDK package.
- MsTest is used with `.runsettings` to specify test run configuration as well as environment details, these files are test project specific.

## Projects

### Smoke Tests

The SmokeTests project consists of black box tests that target MAS, SM and the TransactionAPI. These tests have been selected as the main tests that must pass with every build/release.

Currently this build is triggered by a webhook from AppVeyor for only azure-* branches, the branch and environment urls are sent through for the azure build pipeline to use.

The [Pipeline definition](/azure-pipelines.yml) file defines the build and test runs, here is where the environment details of the azure- branch is passed into the SmokeTests project via the .runsettings file.

The settings file [RunSettings](/smoketests/runSettings/) contains configuration such as the browser for web tests and other settings such as environment details.
The local test settings is recommended if running locally as the video data collector seems to be tricky to set up.

- TestRun.cs is the entry point for the entire test
- Web tests inherit from [WebTestFixture.cs](/smoketests/Tests/WebTestFixture.cs)
- API tests inherit from [ApiTestFixture.cs](/smoketests/Tests/ApiTestFixture.cs)


