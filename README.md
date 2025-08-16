# IS2 Aptitude Test

This repository contains Anna Weber's submission for the IS2 aptitude test described below.

---

# Data Exporter

The **Data Exporter** app is a small RESTful API implemented in .NET 6. It manages insurance policies and any notes the brokers might have added to the policies. It also provides a way to query and map the data to a format an external system might require for importing.

# Tasks

1. The **GetPolicy** method of the **PoliciesController** has already been implemented, but both itself and the **ReadPolicyAsync** function it calls from the service have some logic errors. Find and fix the logic errors and suggest any other improvements you would make to those methods, if any.
2. Implement the **GetPolicies** endpoint that should return all existing policies.
3. Implement the **PostPolicies** endpoint. It should create a new policy based on the data of the DTO it receives in the body of the call and return a read DTO, if successful. 
4. The **Note** entity has been created, but it's not yet configured in the **ExporterDbContext**. Add the missing configuration, considering there is a one-to-many relationship between the **Policy** and the **Note** entities, and seed the database with a few notes.
5. Implement the **Export** endpoint. The call receives two parameters from the query string, the **startDate** and the **endDate**. The method needs to retrieve all policies that have a start date between those two dates, and all of their notes. The data should then be mapped to the **ExportDto** class and returned.

## Remarks

- The tasks can be completed in any order.
- Any third party library can be used to implement some of the functionality required.
- To test the API, any tool like cURL or Postman can be used and the scripts should be included in the submission.

---

# Response

Suggested improvements to **GetPolicy**:
- It could potentially make sense to also include notes for the given policy, depending on the API design and who is calling this and when.

Suggested improvements to **ReadPolicyAsync**:
- Documentation comment had incorrect description

Other notes:
- Test framework like xUnit and add Unit Tests and Integration Tests
- Policies are not connected to a user/person. Doesn't make sense - Assume skipped for brevity.
- Authorisation is missing. You should most likely only be able to access policies that apply to yourself. Similarly, you should probably 
- Currently, Policy Number is not enforced to be unique. That's definitely a problem.
- Better error handling. The service frequently just returns null, regardless of the exact error. Is it malformed input or an internal server error? We can't know, so we can't choose between returning 400 and 500 error codes.
- In Development: API mapping via OpenAPI / Swagger would be very useful

## Test Queries

The test queries are located inside DataExporter/DataExporterTestQueries.http

This file can be opened in Visual Studio and has a list of a few requests to try out.