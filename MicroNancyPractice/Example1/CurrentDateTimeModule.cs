using System;
using Nancy;

namespace Example1
{
    public class CurrentDateTimeModule : NancyModule

    {
        public CurrentDateTimeModule()
        {
            Get("/", _ => DateTime.UtcNow);
        }
    }
}