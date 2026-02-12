# Next Steps

## Issues resolved
- Transformed Bookstore.Domain.csproj to net8.0
- Transformed Bookstore.Data.csproj to net8.0
- Transformed Bookstore.Web.csproj to net8.0
- Transformed Bookstore.Cdk.csproj to net8.0
- Transformed Bookstore.Domain.Tests.csproj to net8.0

## Overview

The transformation appears to be successful with no build errors reported across any of the projects in the solution. All five projects (Bookstore.Data, Bookstore.Domain.Tests, Bookstore.Cdk, Bookstore.Web, and Bookstore.Domain) have compiled without issues.

## Validation Steps

### 1. Verify Target Framework

Confirm that all projects are targeting the intended .NET version:

```bash
dotnet list package --framework
```

Review each `.csproj` file to ensure the `<TargetFramework>` element specifies the correct version (e.g., `net6.0`, `net7.0`, or `net8.0`).

### 2. Run Unit Tests

Execute the test suite to ensure functionality remains intact:

```bash
dotnet test
```

Review test results for any failures or warnings. If tests fail, investigate whether they are due to framework differences or actual regressions.

### 3. Check Package Compatibility

List all NuGet packages and verify they are compatible with the target framework:

```bash
dotnet list package --outdated
dotnet list package --deprecated
```

Update any outdated or deprecated packages:

```bash
dotnet add package <PackageName>
```

### 4. Review Runtime Configuration

Examine `runtimeconfig.json` files (if present) and ensure runtime settings are appropriate for cross-platform deployment. Check for any Windows-specific configurations that may need adjustment.

### 5. Test Database Connectivity

Since the solution includes `Bookstore.Data`, verify database connections work correctly:

- Test connection strings for compatibility across platforms
- Verify Entity Framework migrations (if applicable) execute successfully:

```bash
dotnet ef migrations list --project Bookstore.Data
dotnet ef database update --project Bookstore.Data
```

### 6. Validate Web Application

For the `Bookstore.Web` project:

- Run the application locally:

```bash
dotnet run --project Bookstore.Web
```

- Test all endpoints and functionality through the browser
- Verify static files, views, and client-side assets load correctly
- Check for any platform-specific path issues (e.g., backslashes vs. forward slashes)

### 7. Review CDK Infrastructure Code

For the `Bookstore.Cdk` project:

- Ensure AWS CDK constructs are compatible with the new .NET version
- Synthesize the CloudFormation template to verify no errors occur:

```bash
cd Bookstore.Cdk
cdk synth
```

### 8. Cross-Platform Testing

Test the application on different operating systems:

- Run the solution on Linux (if not already done)
- Run the solution on macOS (if available)
- Verify file path handling, case sensitivity, and line ending differences do not cause issues

### 9. Check for Deprecated APIs

Review compiler warnings for any deprecated API usage:

```bash
dotnet build /warnaserror
```

Address any warnings related to deprecated methods or types that may be removed in future .NET versions.

### 10. Performance Testing

Compare application performance between the legacy and migrated versions:

- Measure startup time
- Test response times for key operations
- Monitor memory usage and garbage collection behavior

## Deployment Preparation

### 1. Create Publish Profiles

Generate optimized release builds for each deployment target:

```bash
dotnet publish Bookstore.Web -c Release -o ./publish/web
```

### 2. Verify Published Output

Examine the published output directory to ensure:

- All necessary dependencies are included
- Configuration files are present and correct
- The application runs from the published directory:

```bash
cd ./publish/web
dotnet Bookstore.Web.dll
```

### 3. Update Documentation

Document the following:

- New target framework version
- Any configuration changes required
- Updated deployment procedures
- Known differences from the legacy version

### 4. Environment Configuration

Ensure environment-specific settings are externalized:

- Use environment variables or configuration providers
- Verify `appsettings.json` and `appsettings.{Environment}.json` are correctly structured
- Test configuration loading in different environments (Development, Staging, Production)

### 5. Monitor for Runtime Issues

After deployment to a test environment:

- Enable detailed logging
- Monitor application logs for exceptions or warnings
- Verify all integrations (databases, external APIs, file systems) function correctly

## Additional Considerations

- Review any custom build scripts or pre/post-build events for platform-specific commands
- Check for hardcoded Windows paths (e.g., `C:\`) and replace with cross-platform alternatives
- Verify any P/Invoke or native library dependencies have cross-platform equivalents
- Test any file I/O operations for case sensitivity issues on Linux/macOS