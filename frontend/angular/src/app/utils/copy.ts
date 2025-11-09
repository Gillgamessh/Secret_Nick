export async function copyToClipboard(text: string): Promise<boolean> {
  try {
    const value = (text ?? '').toString();
    if (!value.trim().length) {
      return false;
    }

    if (!navigator.clipboard?.writeText) {
      return false;
    }

    await navigator.clipboard.writeText(value);
    return true;
  } catch (error) {
    console.error('Failed to copy text to clipboard:', error);
    return false;
  }
}
