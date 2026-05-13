IF DB_ID('ApiSeguraDb') IS NULL
BEGIN
    CREATE DATABASE ApiSeguraDb;
END
GO

USE ApiSeguraDb;
GO

IF OBJECT_ID('Tareas', 'U') IS NOT NULL DROP TABLE Tareas;
IF OBJECT_ID('Usuarios', 'U') IS NOT NULL DROP TABLE Usuarios;
GO

CREATE TABLE Usuarios
(
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    NombreUsuario NVARCHAR(50) NOT NULL,
    Correo NVARCHAR(150) NOT NULL,
    HashContrasena NVARCHAR(MAX) NOT NULL,
    Rol NVARCHAR(20) NOT NULL,
    FechaRegistro DATETIME2 NOT NULL
);
GO

CREATE UNIQUE INDEX IX_Usuarios_NombreUsuario ON Usuarios(NombreUsuario);
CREATE UNIQUE INDEX IX_Usuarios_Correo ON Usuarios(Correo);
GO

CREATE TABLE Tareas
(
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Titulo NVARCHAR(150) NOT NULL,
    Descripcion NVARCHAR(500) NULL,
    Completada BIT NOT NULL,
    FechaCreacion DATETIME2 NOT NULL,
    UsuarioId INT NOT NULL,
    CONSTRAINT FK_Tareas_Usuarios FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id)
);
GO

SELECT Id, Nombre, NombreUsuario, Correo, Rol, FechaRegistro FROM Usuarios;
SELECT Id, Titulo, Completada, FechaCreacion, UsuarioId FROM Tareas;
GO
