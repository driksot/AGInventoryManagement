﻿namespace AGInventoryManagement.WebClient.Areas.Identity.Contracts;

public class AuthenticationModel
{
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
    public string? Username { get; set; }
}
