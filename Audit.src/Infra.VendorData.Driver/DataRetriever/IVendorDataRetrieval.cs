using System.Collections.Generic;
using AuditCore;

namespace Infrastructure.VendorData.Driver;

public interface IVendorDataRetrieval
{
   IEnumerable<VendorRecord> VendorData();
}