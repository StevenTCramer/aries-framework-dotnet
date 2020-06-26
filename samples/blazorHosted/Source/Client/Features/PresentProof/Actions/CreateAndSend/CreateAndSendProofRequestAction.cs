﻿namespace BlazorHosted.Features.PresentProofs
{
  using BlazorHosted.Features.Bases;
  using BlazorHosted.Features.PresentProofs;

  internal partial class PresentProofState
  {
    public class CreateAndSendProofRequestAction : BaseAction
    {
      public SendRequestForProofRequest SendRequestForProofRequest { get; set; }
    }
  }
}
