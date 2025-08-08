use guedestimedatabase;

-- Script de Incicialização
SET FOREIGN_KEY_CHECKS = 0;
DROP PROCEDURE IF EXISTS truncate_all;
DELIMITER //
CREATE PROCEDURE truncate_all()
BEGIN
  DECLARE done INT DEFAULT FALSE;
  DECLARE tab VARCHAR(255);
  DECLARE cur CURSOR FOR
    SELECT table_name FROM information_schema.tables
    WHERE table_schema = DATABASE()
      AND table_type = 'BASE TABLE'
      AND table_name NOT IN (
        '__efmigrationshistory',
        'aspnetroleclaims',
        'aspnetuserclaims',
        'aspnetroles',
        'aspnetuserlogins',
        'aspnetuserroles',
        'aspnetusers',
        'aspnetusertokens'
      );
  DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;

  OPEN cur;
  read_loop: LOOP
    FETCH cur INTO tab;
    IF done THEN
      LEAVE read_loop;
    END IF;
    SET @s = CONCAT('TRUNCATE TABLE `', tab, '`');
    PREPARE stmt FROM @s;
    EXECUTE stmt;
    DEALLOCATE PREPARE stmt;
  END LOOP;
  CLOSE cur;
END;
//
DELIMITER ;
CALL truncate_all();
DROP PROCEDURE truncate_all;
SET FOREIGN_KEY_CHECKS = 1;

-- Insert Instituicao
INSERT INTO `guedestimedatabase`.`instituicoes` 
(`Id`, `Nome`, `CodigoCie`, `Cnpj`, `Avatar`, `Ativo`, `UsuarioId`, `DataCriacao`, `DataAlteracao`)
SELECT 
    x.Id, x.Nome, x.CodigoCie, x.Cnpj, x.Avatar, x.Ativo, u.Id AS UsuarioId, UTC_TIMESTAMP(), NULL
FROM (
    SELECT '11111111-1111-1111-1111-111111111111' AS Id, 'Instituição A' AS Nome, 'CIE001' AS CodigoCie, '12345678000101' AS Cnpj, '/assets/avatar/avatar_1.jpg' AS Avatar, 1 AS Ativo
    UNION ALL SELECT '22222222-2222-2222-2222-222222222222', 'Instituição B', 'CIE002', '23456789000102', '/assets/avatar/avatar_1.jpg', 1
    UNION ALL SELECT '33333333-3333-3333-3333-333333333333', 'Instituição C', 'CIE003', '34567890000103', '/assets/avatar/avatar_1.jpg', 1
    UNION ALL SELECT '44444444-4444-4444-4444-444444444444', 'Instituição D', 'CIE004', '45678901000104', '/assets/avatar/avatar_1.jpg', 1
    UNION ALL SELECT '55555555-5555-5555-5555-555555555555', 'Instituição E', 'CIE005', '56789012000105', '/assets/avatar/avatar_1.jpg', 1
    UNION ALL SELECT '66666666-6666-6666-6666-666666666666', 'Instituição F', 'CIE006', '67890123000106', '/assets/avatar/avatar_1.jpg', 1
    UNION ALL SELECT '77777777-7777-7777-7777-777777777777', 'Instituição G', 'CIE007', '78901234000107', '/assets/avatar/avatar_1.jpg', 1
    UNION ALL SELECT '88888888-8888-8888-8888-888888888888', 'Instituição H', 'CIE008', '89012345000108', '/assets/avatar/avatar_1.jpg', 1
    UNION ALL SELECT '99999999-9999-9999-9999-999999999999', 'Instituição I', 'CIE009', '90123456000109', '/assets/avatar/avatar_1.jpg', 1
    UNION ALL SELECT 'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa', 'Instituição J', 'CIE010', '01234567000110', '/assets/avatar/avatar_1.jpg', 1
) AS x
CROSS JOIN (
    SELECT Id FROM AspNetUsers LIMIT 1
) AS u;


INSERT INTO Enderecos
(Id, InstituicaoId, Logradouro, Numero, Complemento, Cep, Bairro, Cidade, Estado, Ativo, DataCriacao, DataAlteracao) VALUES
( '11111111-1111-1111-1111-111111111111', '11111111-1111-1111-1111-111111111111', 'Rua A', '100', 'Apto 1', '12345-678', 'Centro', 'Cidade A', 'Estado A', TRUE, UTC_TIMESTAMP(), NULL),
( '22222222-2222-2222-2222-222222222222', '22222222-2222-2222-2222-222222222222', 'Rua B', '200', 'Apto 2', '23456-789', 'Bairro B', 'Cidade B', 'Estado B', TRUE, UTC_TIMESTAMP(), NULL),
( '33333333-3333-3333-3333-333333333333', '33333333-3333-3333-3333-333333333333', 'Rua C', '300', 'Apto 3', '34567-890', 'Bairro C', 'Cidade C', 'Estado C', TRUE, UTC_TIMESTAMP(), NULL),
( '44444444-4444-4444-4444-444444444444', '44444444-4444-4444-4444-444444444444', 'Rua D', '400', 'Apto 4', '45678-901', 'Bairro D', 'Cidade D', 'Estado D', TRUE, UTC_TIMESTAMP(), NULL),
( '55555555-5555-5555-5555-555555555555', '55555555-5555-5555-5555-555555555555', 'Rua E', '500', 'Apto 5', '56789-012', 'Bairro E', 'Cidade E', 'Estado E', TRUE, UTC_TIMESTAMP(), NULL),
( '66666666-6666-6666-6666-666666666666', '66666666-6666-6666-6666-666666666666', 'Rua F', '600', 'Apto 6', '67890-123', 'Bairro F', 'Cidade F', 'Estado F', TRUE, UTC_TIMESTAMP(), NULL),
( '77777777-7777-7777-7777-777777777777', '77777777-7777-7777-7777-777777777777', 'Rua G', '700', 'Apto 7', '78901-234', 'Bairro G', 'Cidade G', 'Estado G', TRUE, UTC_TIMESTAMP(), NULL),
( '88888888-8888-8888-8888-888888888888', '88888888-8888-8888-8888-888888888888', 'Rua H', '800', 'Apto 8', '89012-345', 'Bairro H', 'Cidade H', 'Estado H', TRUE, UTC_TIMESTAMP(), NULL),
( '99999999-9999-9999-9999-999999999999', '99999999-9999-9999-9999-999999999999', 'Rua I', '900', 'Apto 9', '90123-456', 'Bairro I', 'Cidade I', 'Estado I', TRUE, UTC_TIMESTAMP(), NULL),
( 'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa', 'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa', 'Rua J', '1000', 'Apto 10', '01234-567', 'Bairro J', 'Cidade J', 'Estado J', TRUE, UTC_TIMESTAMP(), NULL);


-- Series
INSERT INTO Series (Id, Nome, InstituicaoId, TipoEnsino, Ativo, Codigo, DataCriacao, DataAlteracao) VALUES
('554736a0-8a2b-44ba-b639-e8478b563470', '4º', '11111111-1111-1111-1111-111111111111', 1, true, '1', UTC_TIMESTAMP(), NULL),
('25b7b4d9-2513-45b8-8120-45160f46a1ff', '5º', '11111111-1111-1111-1111-111111111111', 1, true, '2', UTC_TIMESTAMP(), NULL),
('2b1c8320-41eb-4361-ac7a-e9fcd65a9b02', '6º', '11111111-1111-1111-1111-111111111111', 2, true, '3', UTC_TIMESTAMP(), NULL),
('7d79a207-29f7-4116-b3ef-6b75c6fdcf56', '7º', '11111111-1111-1111-1111-111111111111', 2, true, '4', UTC_TIMESTAMP(), NULL),
('aed74643-24c7-4dad-ad6c-0f5e21144949', '8º', '11111111-1111-1111-1111-111111111111', 2, true, '5', UTC_TIMESTAMP(), NULL),
('07e916ba-ec37-4445-aba7-ea23b5c7929f', '9º', '11111111-1111-1111-1111-111111111111', 2, true, '6', UTC_TIMESTAMP(), NULL),
('c191f198-281e-4729-a660-cd0a0e0025dc', '1º', '11111111-1111-1111-1111-111111111111', 3, true, '7', UTC_TIMESTAMP(), NULL),
('9adffb96-1ba7-43c3-9f50-bc2a60a9768d', '2º', '11111111-1111-1111-1111-111111111111', 3, true, '8', UTC_TIMESTAMP(), NULL),
('966df081-179d-47cf-8e65-bc2c25cf1ba8', '3º', '11111111-1111-1111-1111-111111111111', 3, true, '9', UTC_TIMESTAMP(), NULL),
('740e5bd0-8b28-43fd-aefc-17346575fcd1', '4º', '11111111-1111-1111-1111-111111111111', 3, true, '10', UTC_TIMESTAMP(), NULL);

-- Disciplinas
INSERT INTO Disciplinas (Id, Nome, InstituicaoId, Ativo,Codigo, DataCriacao, DataAlteracao) VALUES
('e70cb78c-697e-44b5-94ae-4557ee813df5', 'Empreendedorismo', '11111111-1111-1111-1111-111111111111',1, true, UTC_TIMESTAMP(), NULL),
('e454d29c-649f-4971-8bb8-8d50918b310d', 'História', '11111111-1111-1111-1111-111111111111', 2,true, UTC_TIMESTAMP(), NULL),
('dcb93d14-fb1b-454d-988b-85df7c29b0f0', 'Física', '11111111-1111-1111-1111-111111111111', 3,true, UTC_TIMESTAMP(), NULL),
('d68019b3-a60f-49b5-beb7-49999ef21fef', 'Sociologia', '11111111-1111-1111-1111-111111111111',4, true, UTC_TIMESTAMP(), NULL),
('cf84bd44-a7e6-4f72-be9e-512c7a4bd5c1', 'Mat', '11111111-1111-1111-1111-111111111111', 5,true, UTC_TIMESTAMP(), NULL),
('cf128141-5dfe-4269-bee7-42dae39bbe7c', 'Redação', '11111111-1111-1111-1111-111111111111',6, true, UTC_TIMESTAMP(), NULL),
('cd1ead1e-2ae0-4e0b-82d3-b7a3f38781ca', 'Espanhol', '11111111-1111-1111-1111-111111111111',7, true, UTC_TIMESTAMP(), NULL),
('b6b5b121-a1e8-499c-83fe-52657230ae40', 'Artes', '11111111-1111-1111-1111-111111111111',8, true, UTC_TIMESTAMP(), NULL),
('b25cd1e1-f452-41b8-87e6-2935f814df29', 'Educação Física', '11111111-1111-1111-1111-111111111111',9, true, UTC_TIMESTAMP(), NULL),
('a1f02987-6ffc-407d-b50e-74989ef9c78c', 'Robótica', '11111111-1111-1111-1111-111111111111', 11,true, UTC_TIMESTAMP(), NULL),
('9deca854-0cce-459b-828a-da2c4bda67c8', 'Informática', '11111111-1111-1111-1111-111111111111', 12,true, UTC_TIMESTAMP(), NULL),
('8ee519f4-360f-47c4-a8c7-496d64fdb3c0', 'Gramática', '11111111-1111-1111-1111-111111111111', 13,true, UTC_TIMESTAMP(), NULL),
('7b3c09e1-f752-4da0-b2ef-b2259fed528c', 'Inglês', '11111111-1111-1111-1111-111111111111', 14,true, UTC_TIMESTAMP(), NULL),
('761a8c52-4849-41c6-b2ab-5519b8730e89', 'Português', '11111111-1111-1111-1111-111111111111', 15,true, UTC_TIMESTAMP(), NULL),
('70361855-0f14-45b4-b019-4e52bdc36f56', 'Ciências', '11111111-1111-1111-1111-111111111111', 16,true, UTC_TIMESTAMP(), NULL),
('68f6adbd-1fcf-4996-b218-568e25c59ffb', 'Geografia', '11111111-1111-1111-1111-111111111111', 17, true, UTC_TIMESTAMP(), NULL),
('52806534-8d75-433a-b76c-4a0bb26c10f5', 'Filosofia', '11111111-1111-1111-1111-111111111111', 18, true, UTC_TIMESTAMP(), NULL),
('4be16edc-5648-4584-9f5e-0aad60c429d5', 'Literatura', '11111111-1111-1111-1111-111111111111', 19,true, UTC_TIMESTAMP(), NULL),
('151d84e5-43b7-4a77-8e59-358c389809e5', 'Química', '11111111-1111-1111-1111-111111111111', 21, true, UTC_TIMESTAMP(), NULL),
('08a7655a-e589-4a56-bcdd-cc322a1c7dfd', 'Matemática', '11111111-1111-1111-1111-111111111111', 22, true, UTC_TIMESTAMP(), NULL),
('05a0f952-78a7-40e7-887c-a0155c492b0c', 'Biologia', '11111111-1111-1111-1111-111111111111',23, true, UTC_TIMESTAMP(), NULL);

