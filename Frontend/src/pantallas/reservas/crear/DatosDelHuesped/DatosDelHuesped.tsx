import { Autocomplete } from 'components/Autocomplete';
import { Input } from 'components/Input';
import { paisesParaAutocomplete } from 'pantallas/reservas/crear/DatosDelHuesped/ListaDePaises';
import React, { ReactElement } from 'react';
import { DatosMinimosDeHuespedDTO } from 'store/api/DTOs';

interface IProps {
  huesped?: DatosMinimosDeHuespedDTO;
  buscarDniOPasaporte: (dniOPasaporte: string) => void;
}

const DatosDelHuesped = ({ huesped, buscarDniOPasaporte }: IProps): ReactElement => {
  const paisDelHuesped = huesped ? paisesParaAutocomplete.find((x): boolean => x.value === huesped.pais) : undefined;
  const paisOpcionInicial = paisDelHuesped ? paisDelHuesped : paisesParaAutocomplete[8];

  return (
    <div>
      <div className="columns">
        <div className="column is-one-third">
          <Input
            placeholder="DNI o Pasaporte"
            textoDelBoton="Buscar"
            onButtonClick={buscarDniOPasaporte}
            dataCy="dni"
            name="DatosMinimosDeHuesped.DNIOPasaporte"
            type="number"
            defaultValue={huesped?.dniOPasaporte}
            faIconCode="id-card"
          />
        </div>
        <div className="column">
          <Input
            placeholder="Nombre completo"
            dataCy="nombre"
            name="DatosMinimosDeHuesped.NombreCompleto"
            defaultValue={huesped?.nombreCompleto}
            faIconCode="user"
          />
        </div>
      </div>
      <div className="columns">
        <div className="column is-one-quarter">
          <Autocomplete
            key={paisOpcionInicial.value}
            dataCy="pais"
            name="DatosMinimosDeHuesped.Pais"
            opciones={paisesParaAutocomplete}
            opcionInicial={paisOpcionInicial}
            placeholder="Nacionalidad"
            icono="globe"
          />
        </div>
        <div className="column is-one-fifth">
          <Input
            dataCy="telefono"
            name="DatosMinimosDeHuesped.Telefono"
            type="number"
            defaultValue={huesped?.telefono}
            placeholder="Teléfono"
            faIconCode="phone"
          />
        </div>
        <div className="column">
          <Input
            dataCy="email"
            name="DatosMinimosDeHuesped.Email"
            defaultValue={huesped?.email}
            type="email"
            placeholder="Email"
            faIconCode="envelope"
          />
        </div>
      </div>
    </div>
  );
};

export default DatosDelHuesped;
