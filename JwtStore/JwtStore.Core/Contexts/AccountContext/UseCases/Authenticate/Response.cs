﻿using Flunt.Notifications;
using JwtStore.Core.Contexts.SharedContext.UseCases;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate;

public class Response : ResponseBase
{

    protected Response() { }

    public Response(
     string message,
     int status,
     IEnumerable<Notification>? notifications = null)
    {
        Message = message;
        Status = status;
        Notifications = notifications;
    }

    public Response(string message, ResponseData? data)
    {
        Message = message;
        Status = 201;
        Notifications = null;
        Data = data;
    }

    public ResponseData? Data { get; set; }
    public class ResponseData()
    {
        public string Token { get; set; } = String.Empty;
        public string Id { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string[] Roles { get; set; } = Array.Empty<string>();
    }

}