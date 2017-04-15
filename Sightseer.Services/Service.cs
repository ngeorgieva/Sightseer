﻿namespace Sightseer.Services
{
    using SightSeer.Data;

    public abstract class Service
    {
        protected Service()
        {
            this.Context = new SightseerContext();
        }

        protected SightseerContext Context { get; }
    }
}
