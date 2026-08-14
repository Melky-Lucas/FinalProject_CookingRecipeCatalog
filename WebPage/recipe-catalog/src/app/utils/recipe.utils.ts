import { RecipeDifficulty } from '../models/recipe.models';

export function difficultyLabel(value: RecipeDifficulty): string {
  switch (value) {
    case RecipeDifficulty.Easy:
      return 'Fácil';
    case RecipeDifficulty.Medium:
      return 'Media';
    case RecipeDifficulty.Hard:
      return 'Difícil';
    default:
      return 'Desconocida';
  }
}

/** Convierte minutos a formato TimeSpan HH:mm:ss esperado por la API. */
export function minutesToTimeSpan(minutes: number): string {
  const hours = Math.floor(minutes / 60);
  const mins = minutes % 60;
  return `${String(hours).padStart(2, '0')}:${String(mins).padStart(2, '0')}:00`;
}

/** Convierte TimeSpan de la API a minutos para el formulario. */
export function timeSpanToMinutes(timeSpan: string): number {
  if (!timeSpan) {
    return 0;
  }
  const parts = timeSpan.split(':');
  const hours = Number(parts[0]) || 0;
  const minutes = Number(parts[1]) || 0;
  return hours * 60 + minutes;
}

export function formatTimeSpan(timeSpan: string): string {
  const minutes = timeSpanToMinutes(timeSpan);
  if (minutes < 60) {
    return `${minutes} min`;
  }
  const hours = Math.floor(minutes / 60);
  const mins = minutes % 60;
  return mins > 0 ? `${hours} h ${mins} min` : `${hours} h`;
}
