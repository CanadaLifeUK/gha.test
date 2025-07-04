namespace CLUK.Skeleton.API.Query.Policies;

[ApiController]
[Route("[controller]")]
public class PoliciesController(IPoliciesService policiesService)
    : ControllerBase
{
    [HttpGet("{policyNumber}")]
    public async Task<IActionResult> Get(string policyNumber)
    {
        var policyResponse = await policiesService.GetClientsByPolicyNumber(policyNumber);

        if (IsNoContentResponse())
        {
            return NoContent();
        }

        return Ok(policyResponse);

        bool IsNoContentResponse()
        {
            return (policyResponse?.ProductType ?? ProductType.UNKNW) == ProductType.UNKNW ||
                        (policyResponse?.Clients.Count() ?? 0) == 0;
        }
    }
}