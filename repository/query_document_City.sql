/*
create procedure sp_CityCreate(
	@ProvinceID int,
	@CityName varchar(50),
	@CityActive tinyint
)
as 
begin
insert into City (
ProvinceID,
CityName,
CityActive
)values(
@ProvinceID,
@CityName,
@CityActive
);
end;
*/
/*SET IDENTITY_INSERT City ON ;
insert into City (CityID, ProvinceID,CityName,CityActive) values (1, 24, N' 28 de Mayo (San José de Yacuambi)', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (2, 12, N' Alamor', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (3, 5, N' Alausí', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (4, 10, N' Alfredo Baquerizo Moreno (Jujan)', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (5, 12, N' Amaluza', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (6, 23, N' Ambato', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (7, 18, N' Arajuno', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (8, 16, N' Archidona', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (9, 7, N' Arenillas', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (10, 8, N' Atacames', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (11, 11, N' Atuntaqui', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (12, 3, N' Azogues', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (13, 13, N' Baba', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (14, 13, N' Babahoyo', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (15, 16, N' Baeza', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (16, 14, N' Bahía de Caráquez', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (17, 10, N' Balao', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (18, 7, N' Balsas', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (19, 10, N' Balzar', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (20, 23, N' Baños de Agua Santa', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (21, 3, N' Biblián', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (22, 4, N' Bolívar', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (23, 13, N' Buena Fe', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (24, 14, N' Calceta', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (25, 2, N' Caluma', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (26, 1, N' Camilo Ponce Enríquez', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (27, 3, N' Cañar', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (28, 12, N' Cariamanga', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (29, 16, N' Carlos Julio Arosemena Tola', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (30, 12, N' Catacocha', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (31, 12, N' Catamayo', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (32, 13, N' Catarama', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (33, 19, N' Cayambe', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (34, 12, N' Celica', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (35, 23, N' Cevallos', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (36, 12, N' Chaguarpamba', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (37, 5, N' Chambo', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (38, 7, N' Chilla', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (39, 2, N' Chillanes', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (40, 14, N' Chone', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (41, 1, N' Chordeleg', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (42, 5, N' Chunchi', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (43, 10, N' Colimes', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (44, 10, N' Coronel Marcelino Maridueña', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (45, 11, N' Cotacachi', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (46, 5, N' Cumandá', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (47, 10, N' Daule', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (48, 3, N' Déleg', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (49, 10, N' Durán', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (50, 2, N' Echeandía', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (51, 4, N' El Ángel', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (52, 14, N' El Carmen', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (53, 16, N' El Chaco', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (54, 6, N' El Corazón', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (55, 22, N' El Dorado de Cascales', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (56, 7, N' El Guabo', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (57, 1, N' El Pan', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (58, 24, N' El Pangui', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (59, 3, N' El Tambo', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (60, 10, N' El Triunfo', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (61, 8, N' Esmeraldas', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (62, 14, N' Flavio Alfaro', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (63, 10, N' General Antonio Elizalde (Bucay)', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (64, 15, N' General Leonidas Plaza Gutiérrez (Limón)', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (65, 10, N' General Villamil (Playas)', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (66, 1, N' Girón', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (67, 12, N' Gonzanamá', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (68, 1, N' Guachapala', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (69, 1, N' Gualaceo', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (70, 15, N' Gualaquiza', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (71, 5, N' Guamote', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (72, 5, N' Guano', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (73, 2, N' Guaranda', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (74, 10, N' Guayaquil', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (75, 24, N' Guayzimi', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (76, 4, N' Huaca', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (77, 15, N' Huamboya', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (78, 7, N' Huaquillas', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (79, 11, N' Ibarra', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (80, 10, N' Isidro Ayora', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (81, 14, N' Jama', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (82, 14, N' Jaramijó', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (83, 14, N' Jipijapa', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (84, 14, N' Junín', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (85, 22, N' La Bonita', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (86, 21, N' La Concordia', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (87, 17, N' La Joya de los Sachas', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (88, 20, N' La Libertad', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (89, 6, N' La Maná', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (90, 3, N' La Troncal', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (91, 7, N' La Victoria', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (92, 2, N' Las Naves', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (93, 6, N' Latacunga', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (94, 15, N' Logroño', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (95, 12, N' Loja', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (96, 10, N' Lomas de Sargentillo', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (97, 17, N' Loreto', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (98, 22, N' Lumbaqui', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (99, 12, N' Macará', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (100, 15, N' Macas', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (101, 19, N' Machachi', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (102, 7, N' Machala', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (103, 14, N' Manta', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (104, 7, N' Marcabelí', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (105, 18, N' Mera', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (106, 10, N' Milagro', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (107, 4, N' Mira', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (108, 13, N' Mocache', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (109, 13, N' Montalvo', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (110, 14, N' Montecristi', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (111, 8, N' Muisne', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (112, 1, N' Nabón', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (113, 10, N' Naranjal', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (114, 10, N' Naranjito', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (115, 10, N' Narcisa de Jesús (Nobol)', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (116, 22, N' Nueva Loja', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (117, 17, N' Nuevo Rocafuerte', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (118, 12, N' Olmedo', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (119, 14, N' Olmedo', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (120, 11, N' Otavalo', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (121, 15, N' Pablo Sexto', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (122, 7, N' Paccha', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (123, 14, N' Paján', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (124, 24, N' Palanda', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (125, 13, N' Palenque', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (126, 10, N' Palestina', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (127, 5, N' Pallatanga', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (128, 15, N' Palora', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (129, 24, N' Paquisha', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (130, 7, N' Pasaje', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (131, 23, N' Patate', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (132, 1, N' Paute', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (133, 14, N' Pedernales', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (134, 10, N' Pedro Carbo', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (135, 19, N' Pedro Vicente Maldonado', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (136, 23, N' Pelileo', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (137, 5, N' Penipe', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (138, 14, N' Pichincha', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (139, 23, N' Píllaro', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (140, 11, N' Pimampiro', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (141, 12, N' Pindal', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (142, 7, N' Piñas', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (143, 7, N' Portovelo', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (144, 14, N' Portoviejo', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (145, 1, N' Pucará', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (146, 13, N' Puebloviejo', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (147, 9, N' Puerto Ayora', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (148, 9, N' Puerto Baquerizo Moreno', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (149, 22, N' Puerto El Carmen de Putumayo', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (150, 17, N' Puerto Francisco de Orellana', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (151, 14, N' Puerto López', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (152, 19, N' Puerto Quito', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (153, 9, N' Puerto Villamil', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (154, 6, N' Pujilí', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (155, 18, N' Puyo', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (156, 23, N' Quero', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (157, 13, N' Quevedo', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (158, 12, N' Quilanga', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (159, 13, N' Quinsaloma', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (160, 19, N' Quito', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (161, 5, N' Riobamba', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (162, 8, N' Rioverde', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (163, 14, N' Rocafuerte', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (164, 8, N' Rosa Zárate (Quinindé)', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (165, 20, N' Salinas', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (166, 10, N' Salitre', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (167, 10, N' Samborondón', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (168, 1, N' San Felipe de Oña', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (169, 1, N' San Fernando', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (170, 4, N' San Gabriel', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (171, 2, N' San José de Chimbo', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (172, 15, N' San Juan Bosco', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (173, 8, N' San Lorenzo', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (174, 2, N' San Miguel', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (175, 6, N' San Miguel (Salcedo)', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (176, 19, N' San Miguel de Los Bancos', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (177, 14, N' San Vicente', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (178, 19, N' Sangolquí', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (179, 14, N' Santa Ana de Vuelta Larga', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (180, 18, N' Santa Clara', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (181, 20, N' Santa Elena', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (182, 1, N' Santa Isabel', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (183, 10, N' Santa Lucía', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (184, 7, N' Santa Rosa', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (185, 15, N' Santiago', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (186, 15, N' Santiago de Méndez', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (187, 21, N' Santo Domingo', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (188, 6, N' Saquisilí', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (189, 12, N' Saraguro', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (190, 1, N' Sevilla de Oro', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (191, 22, N' Shushufindi', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (192, 6, N' Sigchos', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (193, 1, N' Sígsig', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (194, 10, N' Simón Bolívar', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (195, 12, N' Sozoranga', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (196, 14, N' Sucre', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (197, 15, N' Sucúa', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (198, 3, N' Suscal', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (199, 19, N' Tabacundo', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (200, 15, N' Taisha', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (201, 22, N' Tarapoa', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (202, 16, N' Tena', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (203, 17, N' Tiputini', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (204, 23, N' Tisaleo', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (205, 14, N' Tosagua', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (206, 4, N' Tulcán', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (207, 11, N' Urcuquí', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (208, 8, N' Valdez (Limones)', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (209, 13, N' Valencia', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (210, 10, N' Velasco Ibarra (El Empalme)', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (211, 13, N' Ventanas', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (212, 5, N' Villa La Unión (Cajabamba)', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (213, 13, N' Vinces', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (214, 10, N' Yaguachi', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (215, 24, N' Yantzaza', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (216, 24, N' Zamora', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (217, 12, N' Zapotillo', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (218, 7, N' Zaruma', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (219, 24, N' Zumba', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (220, 24, N' Zumbi', 1)
insert into City (CityID, ProvinceID,CityName,CityActive) values (221, 23, N'Mocha', 1)
SET IDENTITY_INSERT City OFF;
*/
--*****************************************************************************
/*
create procedure sp_CityRead(
	@ProvinceID int=NULL,
	@CityActive tinyint=NULL,
	@Page int,
@Quantity int
)
as 
begin
declare @start int;
set @start = (@Page-1) * @Quantity;
select 
CityID,
ProvinceID,
CityName,
CityActive
from City
where (@ProvinceID IS NULL or ProvinceID = @ProvinceID)
and (@CityActive IS NULL or CityActive = @CityActive)
order by CityName
OFFSET @Start ROWS
FETCH NEXT @Quantity ROWS ONLY;
end;
*/
--exec sp_CityRead NULL, NULL,1,10

--**********************************************************************
/*
create procedure sp_CityCountRead(
@RowsCount bigint out
)
as
begin;
set @RowsCount = (select count(1) from City);
end;
*/
/*
declare @valor bigint
exec sp_CityCountRead @valor out
select @valor
*/
--**********************************************************************
/*
create procedure sp_CityUpdate(
	@CityID int,
	@ProvinceID int,
	@CityName varchar(50),
	@CityActive tinyint
)
as 
begin
update City 
set ProvinceID=@ProvinceID,
CityName = @CityName,
CityActive = @CityActive
where CityID = @CityID;
end;
*/
--exec sp_CityUpdate 17, 10, ' Balao222', 1;

--******************************************************************
/*
create procedure sp_CityDelete(
	@CityID int
)
as 
begin
delete from City 
where CityID = @CityID;
end;
*/

--exec sp_CityDelete 17
