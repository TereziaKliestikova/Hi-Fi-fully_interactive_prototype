export {};

declare global {
  interface Window {
    env?: {
      apiUrl?: string;
    };
  }
}
