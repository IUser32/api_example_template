IF DB_ID('ApiEjemploDb') IS NULL
BEGIN
    CREATE DATABASE ApiEjemploDb;
END
GO

USE ApiEjemploDb;
GO

IF OBJECT_ID('Ordenes', 'U') IS NOT NULL DROP TABLE Ordenes;
IF OBJECT_ID('Productos', 'U') IS NOT NULL DROP TABLE Productos;
IF OBJECT_ID('Proveedores', 'U') IS NOT NULL DROP TABLE Proveedores;
IF OBJECT_ID('Categorias', 'U') IS NOT NULL DROP TABLE Categorias;
IF OBJECT_ID('Clientes', 'U') IS NOT NULL DROP TABLE Clientes;
GO

CREATE TABLE Clientes
(
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Correo NVARCHAR(150) NOT NULL,
    Telefono NVARCHAR(25) NOT NULL
);
GO

CREATE TABLE Categorias
(
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(250) NOT NULL
);
GO

CREATE TABLE Proveedores
(
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Correo NVARCHAR(150) NOT NULL,
    Telefono NVARCHAR(25) NOT NULL
);
GO

CREATE TABLE Productos
(
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Precio DECIMAL(18,2) NOT NULL,
    Existencia INT NOT NULL,
    CategoriaId INT NOT NULL,
    ProveedorId INT NOT NULL,
    CONSTRAINT FK_Productos_Categorias FOREIGN KEY (CategoriaId) REFERENCES Categorias(Id),
    CONSTRAINT FK_Productos_Proveedores FOREIGN KEY (ProveedorId) REFERENCES Proveedores(Id)
);
GO

CREATE TABLE Ordenes
(
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    ClienteId INT NOT NULL,
    Fecha DATETIME2 NOT NULL,
    Total DECIMAL(18,2) NOT NULL,
    Estado NVARCHAR(50) NOT NULL,
    CONSTRAINT FK_Ordenes_Clientes FOREIGN KEY (ClienteId) REFERENCES Clientes(Id)
);
GO

INSERT INTO Clientes (Nombre, Correo, Telefono)
VALUES
('Ana Perez', 'ana.perez@programacion.com', '809-555-1234'),
('Carlos Gomez', 'carlos.gomez@programacion.com', '809-555-5678'),
('Maria Rodriguez', 'maria.rodriguez@programacion.com', '809-555-9012');
GO

INSERT INTO Categorias (Nombre, Descripcion)
VALUES
('Tecnologia', 'Productos electronicos y accesorios'),
('Oficina', 'Articulos para trabajo y estudio'),
('Hogar', 'Productos para uso domestico');
GO

INSERT INTO Proveedores (Nombre, Correo, Telefono)
VALUES
('Tech Supply', 'ventas@programacion.com', '809-555-1111'),
('Ofi Mundo', 'oficina@programacion.com', '809-555-2222'),
('Casa Norte', 'hogar@programacion.com', '809-555-3333');
GO

INSERT INTO Productos (Nombre, Precio, Existencia, CategoriaId, ProveedorId)
VALUES
('Laptop', 45000.00, 8, 1, 1),
('Mouse', 850.00, 25, 1, 1),
('Silla de oficina', 7800.00, 12, 2, 2),
('Lampara', 1600.00, 18, 3, 3);
GO

INSERT INTO Ordenes (ClienteId, Fecha, Total, Estado)
VALUES
(1, '2026-05-12T09:30:00', 45850.00, 'Completada'),
(2, '2026-05-12T10:15:00', 7800.00, 'Pendiente'),
(3, '2026-05-12T11:00:00', 1600.00, 'Completada');
GO

SELECT Id, Nombre, Correo, Telefono FROM Clientes;
SELECT Id, Nombre, Descripcion FROM Categorias;
SELECT Id, Nombre, Correo, Telefono FROM Proveedores;
SELECT Id, Nombre, Precio, Existencia, CategoriaId, ProveedorId FROM Productos;
SELECT Id, ClienteId, Fecha, Total, Estado FROM Ordenes;
GO
