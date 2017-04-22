namespace Sightseer.Services
{
    using Sightseer.Data;

    public abstract class Service
    {
        protected Service()
        {
            this.Context = new SightseerContext();
        }

        protected SightseerContext Context { get; }
    }
}
