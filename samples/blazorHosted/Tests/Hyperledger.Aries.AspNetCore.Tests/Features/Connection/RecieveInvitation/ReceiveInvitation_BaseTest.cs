namespace Hyperledger.Aries.AspNetCore.Server.Integration.Tests.Infrastructure
{
  using Hyperledger.Aries.AspNetCore.Features.Connections;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  public partial class BaseTest
  {
    internal static RecieveInvitationRequest CreateValidRecieveInvitationRequest()
    {

      return new RecieveInvitationRequest(aInvitationDetails: "Todo valid stuff ");
      
    }
  }
}
