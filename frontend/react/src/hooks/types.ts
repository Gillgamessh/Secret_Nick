export interface FetchParams<T> extends RequestInit {
  url: string;
  onSuccess?: (data: T) => void;
  onError?: (error: Error) => void;
}

export interface UseFetchReturn<T, N = undefined> {
  data: T | null;
  isLoading: boolean;
  isError: boolean;
  fetchData: (data?: N) => Promise<void>;
}
