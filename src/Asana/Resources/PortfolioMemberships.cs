using Asana.Models;
using Asana.Requests;

namespace Asana.Resources
{
    public sealed class PortfolioMemberships : Resource
    {
        private readonly uint? _defaultPageSize;

        internal PortfolioMemberships(Dispatcher dispatcher, uint? defaultPageSize) : base(dispatcher)
        {
            _defaultPageSize = defaultPageSize;
        }

        public GetItemsCollectionRequest<PortfolioMembership> GetByPortfolio(string portfolioGid)
        {
            return new GetItemsCollectionRequest<PortfolioMembership>(Dispatcher, _defaultPageSize, "portfolio_memberships")
                .AddQueryParameter("portfolio", portfolioGid);
        }

        public GetItemsCollectionRequest<PortfolioMembership> GetByPortfolioAndUser(string portfolioGid, string userGid)
        {
            return new GetItemsCollectionRequest<PortfolioMembership>(Dispatcher, _defaultPageSize, "portfolio_memberships")
                .AddQueryParameter("portfolio", portfolioGid)
                .AddQueryParameter("user", userGid);
        }

        public GetItemsCollectionRequest<PortfolioMembership> GetByWorkspaceAndUser(string workspaceGid, string userGid)
        {
            return new GetItemsCollectionRequest<PortfolioMembership>(Dispatcher, _defaultPageSize, "portfolio_memberships")
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
                _defaultPageSize,
                $"portfolios/{portfolioGid}/portfolio_memberships");
        }

        public GetItemsCollectionRequest<PortfolioMembership> GetFromPortfolio(string portfolioGid, string userGid) =>
            GetFromPortfolio(portfolioGid).AddQueryParameter("user", userGid);
    }
}