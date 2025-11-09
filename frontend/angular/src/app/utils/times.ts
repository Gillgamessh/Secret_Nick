export function toTimestamp(
  value: string | number | Date | null | undefined
): number {
  if (value == null || value === '') return 0;

  if (typeof value === 'number') return value;
  if (value instanceof Date) return value.getTime();

  const parsed = Date.parse(value);
  return Number.isNaN(parsed) ? 0 : parsed;
}
