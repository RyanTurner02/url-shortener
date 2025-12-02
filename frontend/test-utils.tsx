import QueryProvider from "./providers/query-provider";

export const QueryProviderWrapper = ({children}: {children: React.ReactNode}) => {
    return(
        <QueryProvider>
            {children}
        </QueryProvider>
    );
}