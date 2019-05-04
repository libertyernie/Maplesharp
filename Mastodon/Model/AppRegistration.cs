﻿using Newtonsoft.Json;

namespace Maplesharp.Model
{
    public class AppRegistration
    {
        [JsonProperty("id")] public long Id { get; set; }

        [JsonProperty("redirect_uri")] public string RedirectUri { get; set; }

        [JsonProperty("client_id")] public string ClientId { get; set; }

        [JsonProperty("client_secret")] public string ClientSecret { get; set; }

        [JsonIgnore] public string Instance { get; set; }

        [JsonIgnore] public Scope Scope { get; set; }
    }
}