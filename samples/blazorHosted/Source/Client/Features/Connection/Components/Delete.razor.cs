﻿namespace BlazorHosted.Features.Connections.Components
{
  using Microsoft.AspNetCore.Components;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  public partial class Delete
  {
    [Parameter] public string ConnectionId { get; set; }
  }
}
