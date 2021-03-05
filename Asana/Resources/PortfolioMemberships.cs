using Asana.Dispatchers;
using Asana.Models;
using Asana.Requests;

namespace Asana.Resources
{
    public sealed class PortfolioMemberships : Resource
    {
        internal PortfolioMemberships(Dispatcher dispatcher) : base(dispatcher)
        {
        }

        public GetItemsCollectionRequest<PortfolioMembership> GetByPortfolio(string portfolioGid)
        {
            return new GetItemsCollectionRequest<PortfolioMembership>(Dispatcher, "portfolio_memberships")
                .AddQueryParameter("portfolio", portfolioGid);
        }

        public GetItemsCollectionRequest<PortfolioMembership> GetByPortfolioAndUser(string portfolioGid, string userGid)
        {
            return new GetItemsCollectionRequest<PortfolioMembership>(Dispatcher, "portfolio_memberships")
                .AddQueryParameter("portfolio", portfolioGid)
                .AddQueryParameter("user", userGid);
        }

        public GetItemsCollectionRequest<PortfolioMembership> GetByWorkspaceAndUser(string workspaceGid, string userGid)
        {
            return new GetItemsCollectionRequest<PortfolioMembership>(Dispatcher, "portfolio_memberships")
                .AddQueryParameter("workspace", workspaceGid)
                .AddQueryParameter("user", userGid);
        }

        public GetItemRequest<PortfolioMembership> Get(string portfolioMembershipGid)
        {
            return new GetItemRequest<PortfolioMembership>(Dispatcher,
                $"portfolio_memberships/{portfolioMembershipGid}");
        }

        public GetItemsCollectionRequest<PortfolioMembership> GetFromPortfolio(string portfolioGid)
        {
            return new GetItemsCollectionRequest<PortfolioMembership>(Dispatcher,
                $"portfolios/{portfolioGid}/portfolio_memberships");
        }

        public GetItemsCollectionRequest<PortfolioMembership> GetFromPortfolio(string portfolioGid, string userGid) =>
            GetFromPortfolio(portfolioGid).AddQueryParameter("user", userGid);
    }
}