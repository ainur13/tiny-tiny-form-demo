export function ariaInvalid(errors: Record<string, string | undefined>, field: string): 'true' | undefined {
  return errors[field] ? 'true' : undefined
}