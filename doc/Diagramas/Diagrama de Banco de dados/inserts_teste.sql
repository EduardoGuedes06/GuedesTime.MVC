INSERT INTO `guedestimedatabase`.`instituicoes` (`Id`, `Nome`, `CodigoCie`, `Cnpj`, `Avatar`, `Ativo`, `UsuarioId`) VALUES
('11111111-1111-1111-1111-111111111111', 'Instituição A', 'CIE001', '12345678000101', '/assets/avatar/avatar_1.jpg', 1, '2686e2b0-5de1-4685-9fdf-c2e58ab13797'),
('22222222-2222-2222-2222-222222222222', 'Instituição B', 'CIE002', '23456789000102', '/assets/avatar/avatar_1.jpg', 1, '2686e2b0-5de1-4685-9fdf-c2e58ab13797'),
('33333333-3333-3333-3333-333333333333', 'Instituição C', 'CIE003', '34567890000103', '/assets/avatar/avatar_1.jpg', 1, '2686e2b0-5de1-4685-9fdf-c2e58ab13797'),
('44444444-4444-4444-4444-444444444444', 'Instituição D', 'CIE004', '45678901000104', '/assets/avatar/avatar_1.jpg', 1, '2686e2b0-5de1-4685-9fdf-c2e58ab13797'),
('55555555-5555-5555-5555-555555555555', 'Instituição E', 'CIE005', '56789012000105', '/assets/avatar/avatar_1.jpg', 1, '2686e2b0-5de1-4685-9fdf-c2e58ab13797'),
('66666666-6666-6666-6666-666666666666', 'Instituição F', 'CIE006', '67890123000106', '/assets/avatar/avatar_1.jpg', 1, '2686e2b0-5de1-4685-9fdf-c2e58ab13797'),
('77777777-7777-7777-7777-777777777777', 'Instituição G', 'CIE007', '78901234000107', '/assets/avatar/avatar_1.jpg', 1, '2686e2b0-5de1-4685-9fdf-c2e58ab13797'),
('88888888-8888-8888-8888-888888888888', 'Instituição H', 'CIE008', '89012345000108', '/assets/avatar/avatar_1.jpg', 1, '2686e2b0-5de1-4685-9fdf-c2e58ab13797'),
('99999999-9999-9999-9999-999999999999', 'Instituição I', 'CIE009', '90123456000109', '/assets/avatar/avatar_1.jpg', 1, '2686e2b0-5de1-4685-9fdf-c2e58ab13797'),
('aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa', 'Instituição J', 'CIE010', '01234567000110', '/assets/avatar/avatar_1.jpg', 1, '2686e2b0-5de1-4685-9fdf-c2e58ab13797');

INSERT INTO `guedestimedatabase`.`enderecos` (`Id`, `InstituicaoId`, `Logradouro`, `Numero`, `Complemento`, `Cep`, `Bairro`, `Cidade`, `Estado`) VALUES
(UUID(), '11111111-1111-1111-1111-111111111111', 'Rua A', '100', 'Apto 1', '12345-678', 'Centro', 'Cidade A', 'Estado A'),
(UUID(), '22222222-2222-2222-2222-222222222222', 'Rua B', '200', 'Apto 2', '23456-789', 'Bairro B', 'Cidade B', 'Estado B'),
(UUID(), '33333333-3333-3333-3333-333333333333', 'Rua C', '300', 'Apto 3', '34567-890', 'Bairro C', 'Cidade C', 'Estado C'),
(UUID(), '44444444-4444-4444-4444-444444444444', 'Rua D', '400', 'Apto 4', '45678-901', 'Bairro D', 'Cidade D', 'Estado D'),
(UUID(), '55555555-5555-5555-5555-555555555555', 'Rua E', '500', 'Apto 5', '56789-012', 'Bairro E', 'Cidade E', 'Estado E'),
(UUID(), '66666666-6666-6666-6666-666666666666', 'Rua F', '600', 'Apto 6', '67890-123', 'Bairro F', 'Cidade F', 'Estado F'),
(UUID(), '77777777-7777-7777-7777-777777777777', 'Rua G', '700', 'Apto 7', '78901-234', 'Bairro G', 'Cidade G', 'Estado G'),
(UUID(), '88888888-8888-8888-8888-888888888888', 'Rua H', '800', 'Apto 8', '89012-345', 'Bairro H', 'Cidade H', 'Estado H'),
(UUID(), '99999999-9999-9999-9999-999999999999', 'Rua I', '900', 'Apto 9', '90123-456', 'Bairro I', 'Cidade I', 'Estado I'),
(UUID(), 'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa', 'Rua J', '1000', 'Apto 10', '01234-567', 'Bairro J', 'Cidade J', 'Estado J');
