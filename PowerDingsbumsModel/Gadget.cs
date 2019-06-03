namespace PowerDingsbumsModel {
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    using System;

    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class Gadget {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
