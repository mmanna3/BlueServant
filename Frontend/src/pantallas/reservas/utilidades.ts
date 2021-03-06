import { CamaDTO } from 'store/api/DTOs';

export interface IHabitacionParaTablaReservas {
  id: number;
  estaHabilitada: boolean;
  nombre: string;
  esPrivada: boolean;
  camas: CamaDTO[];
}
