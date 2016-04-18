# Redis-AspNet-Azure

Example code of using Redis cache with AspNet on Azure.

## Getting Started

This sample application uses the RedisRepo library which, in turn, uses the StackExchange.Redis library to interact with Redis. The specific areas of note are located in the following files:

    - src/RedisOnAzure/RedisOnAzure.Web/App_Cache
    - src/RedisOnAzure/RedisOnAzure.Web/App_Start/RedisConfiguration.cs
    - src/RedisOnAzure/RedisOnAzure.Web/App_Start/RedisSessionStateConfig.cs
    - src/RedisOnAzure/RedisOnAzure.Web/Controllers/HomeController.cs (Index method)
    - src/RedisOnAzure/RedisOnAzure.Web/Controllers/AccountController.cs (Login method and ForgotPassword method)
