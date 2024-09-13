CREATE DATABASE DirectBarber1;
USE DirectBarber1;


CREATE TABLE Rol (
    Id INT PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL
);

SELECT * FROM Rol;


CREATE TABLE Usuario (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(50),
    apellido NVARCHAR(50),
    correo NVARCHAR(60) NOT NULL,
    contrasena NVARCHAR(200) NOT NULL,
    direccion NVARCHAR(50),
    telefono NVARCHAR(20),
    fec_registro DATETIME DEFAULT GETDATE(),
    fec_nacimiento DATE,
    calificacion DECIMAL(3,2),
    foto VARBINARY(MAX),
    documento NVARCHAR(10),
    id_Rol INT NOT NULL,
    FOREIGN KEY (id_Rol) REFERENCES Rol(Id)
);

SELECT * FROM Usuario;


UPDATE Rol
SET Id = 2
WHERE Id = 3;

INSERT INTO Rol (Id, Nombre)
VALUES (1,'Cliente'), (2, 'Barbero');

select * from Rol;
/*Recomendacion de ChatGPT tabla solicitud*/
CREATE TABLE Solicitud (
    id_Solicitud INT IDENTITY(1,1) PRIMARY KEY,
    id_Cliente INT NULL,
    id_Barbero INT NULL,
	Dirección NVarchar(MAX),
    fecha DATETIME,
    descripcion NVARCHAR(200),
    precio DECIMAL(10,2),
    FOREIGN KEY (id_Cliente) REFERENCES Usuario(Id),
    FOREIGN KEY (id_Barbero) REFERENCES Usuario(Id)
);

select * from Solicitud;

DROP TABLE Solicitud;
/*Consulta para obtener el barbero.*/
SELECT * 
FROM Usuario u
JOIN Rol r ON u.id_Rol = r.Id
WHERE r.Nombre = 'Barbero';


/*Consulta para obtener el barbero.*/
SELECT * 
FROM Usuario u
JOIN Rol r ON u.id_Rol = r.Id
WHERE r.Nombre = 'Cliente';


/*Cear un usuario y un barbero*/
INSERT INTO Usuario (nombre, apellido, correo, contrasena, direccion, telefono, fec_registro, fec_nacimiento, calificacion, foto, documento, id_Rol)
VALUES ('Juan', 'Pérez', 'juan.perez@example.com', 'contrasena123', 'Calle Falsa 123', '555-1234', DEFAULT, '1990-05-15', 4.5, NULL, '1234567890', 1);

INSERT INTO Usuario (nombre, apellido, correo, contrasena, direccion, telefono, fec_registro, fec_nacimiento, calificacion, foto, documento, id_Rol)
VALUES ('Pedro', 'Gómez', 'pedro.gomez@example.com', 'contrasena456', 'Avenida Siempre Viva 742', '555-5678', DEFAULT, '1985-08-22', 4.8, NULL, '0987654321', 2);
