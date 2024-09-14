CREATE DATABASE Playground;

USE Playground;

CREATE TABLE Notes
(
    Id INT IDENTITY(100, 1) PRIMARY KEY,
    Title VARCHAR(255),
    Body VARCHAR(max)
);
