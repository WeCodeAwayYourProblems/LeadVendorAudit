using CoreLogic;

namespace AuditInfrastructure;

public class VendorRecord : IVendorRecord
{
   public required string VendorName { get; init; }
}