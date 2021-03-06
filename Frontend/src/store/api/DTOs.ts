// ..\Backend\Api\Controllers\DTOs\Habitacion\CamaCuchetaDTO.cs
export interface CamaCuchetaDTO {
    id: number;
    nombre: string;
    abajo: CamaDTO;
    arriba: CamaDTO;
}

// ..\Backend\Api\Controllers\DTOs\Habitacion\CamaDTO.cs
export interface CamaDTO {
    id: number;
    estaHabilitada: boolean;
    nombre: string;
    tipo: CamaTipoEnum;
    nombreHabitacion: string;
}

// ..\Backend\Api\Controllers\DTOs\Habitacion\CambiarHabilitacionDTO.cs
export interface CambiarHabilitacionDTO {
    id: number;
}

// ..\Backend\Api\Controllers\DTOs\Habitacion\HabitacionBaseDTO.cs
export interface HabitacionBaseDTO {
    id: number;
    nombre: string;
    tieneBanio: boolean;
    esPrivada: boolean;
    estaHabilitada: boolean;
    informacionAdicional: string;
}

// ..\Backend\Api\Controllers\DTOs\Habitacion\HabitacionConLugaresLibresDTO.cs
export interface HabitacionConLugaresLibresDTO {
    id: number;
    nombre: string;
    esPrivada: boolean;
    camas: CamaDTO[];
    cantidadDeLugaresLibres: number;
}

// ..\Backend\Api\Controllers\DTOs\Habitacion\HabitacionDetalleDTO.cs
export interface HabitacionDetalleDTO extends HabitacionBaseDTO {
    camas: CamaDTO[];
}

// ..\Backend\Api\Controllers\DTOs\Habitacion\HabitacionDTO.cs
export interface HabitacionDTO extends HabitacionBaseDTO {
    camasIndividuales: CamaDTO[];
    camasCuchetas: CamaCuchetaDTO[];
    camasMatrimoniales: CamaDTO[];
}

// ..\Backend\Api\Controllers\DTOs\Pasajero\PasajeroDTO.cs
export interface PasajeroDTO {
    id: number;
    nombreCompleto: string;
    dniOPasaporte: string;
    pais: string;
    telefono: string;
    email: string;
}

// ..\Backend\Api\Controllers\DTOs\Reserva\CancelarDTO.cs
export interface CancelarDTO {
    reservaId: number;
}

// ..\Backend\Api\Controllers\DTOs\Reserva\CantidadCheckInsYCheckOutsDeHoyDTO.cs
export interface CantidadCheckInsYCheckOutsDeHoyDTO {
    checkIns: number;
    checkOuts: number;
}

// ..\Backend\Api\Controllers\DTOs\Reserva\CheckoutsDeHoyDTO.cs
export interface CheckoutsDeHoyDTO {
    id: number;
    aNombreDe: string;
}

// ..\Backend\Api\Controllers\DTOs\Reserva\HacerCheckInDTO.cs
export interface HacerCheckInDTO {
    reservaId: number;
    pasajeroTitular: PasajeroDTO;
    pasajerosAnexos: PasajeroDTO[];
}

// ..\Backend\Api\Controllers\DTOs\Reserva\HacerCheckOutDTO.cs
export interface HacerCheckOutDTO {
    reservaId: number;
}

// ..\Backend\Api\Controllers\DTOs\Reserva\ReservaBaseDTO.cs
export interface ReservaBaseDTO {
    id: number;
    estado: ReservaEstadoEnum;
    horaEstimadaDeLlegada: string;
    cantidadDePasajeros: number;
    canal: string;
    pasajeroTitular: PasajeroDTO;
    diaDeCheckin: string;
    diaDeCheckout: string;
}

// ..\Backend\Api\Controllers\DTOs\Reserva\ReservaCreacionDTO.cs
export interface ReservaCreacionDTO extends ReservaBaseDTO {
    camasIds: number[];
    habitacionesPrivadasIds: number[];
}

// ..\Backend\Api\Controllers\DTOs\Reserva\ReservaDetalleDTO.cs
export interface ReservaDetalleDTO extends ReservaBaseDTO {
    camas: CamaDTO[];
    habitacionesPrivadas: HabitacionDTO[];
    pasajerosAnexos: PasajeroDTO[];
}

// ..\Backend\Api\Controllers\DTOs\Reserva\ReservaResumenDTO.cs
export interface ReservaResumenDTO {
    id: number;
    pasejeroTitular: string;
    nombreAbreviadoDelPasajero: string;
    estado: ReservaEstadoEnum;
    diaDeCheckin: string;
    diaDeCheckout: string;
    camasIds: number[];
}

// ..\Backend\Api\Controllers\DTOs\Reserva\ReservasDelPeriodoDTO.cs
export interface ReservasDelPeriodoDTO {
    reservas: ReservaResumenDTO[];
    desde: string;
    hasta: string;
}

// ..\Backend\Api\Controllers\DTOs\Usuario\AutenticarDTO.cs
export interface AutenticarDTO {
    username: string;
    password: string;
}

// ..\Backend\Api\Controllers\DTOs\Usuario\RegistrarDTO.cs
export interface RegistrarDTO {
    nombre: string;
    apellido: string;
    username: string;
    password: string;
}

// ..\Backend\Api\Core\Enums\CamaTipoEnum.cs
export enum CamaTipoEnum {
    Individual = 1,
    Matrimonial = 2,
    CuchetaArriba = 3,
    CuchetaAbajo = 4,
}

// ..\Backend\Api\Core\Enums\HabitacionTipoEnum.cs
export enum HabitacionTipoEnum {
    Compartida = 1,
    Privada = 2,
}

// ..\Backend\Api\Core\Enums\ReservaEstadoEnum.cs
export enum ReservaEstadoEnum {
    Cancelada = 0,
    CheckinPendiente = 1,
    InHouse = 2,
    HizoCheckout = 3,
}
