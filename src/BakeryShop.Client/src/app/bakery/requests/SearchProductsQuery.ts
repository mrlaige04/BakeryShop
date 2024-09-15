export interface SearchProductsQuery {
  query?: string | undefined;
  priceFrom?: number | undefined;
  priceTo?: number | undefined;
  pageNumber?: number | undefined;
  pageSize?: number | undefined;
}
