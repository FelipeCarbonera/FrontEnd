
--
-- CRIAR TABELA PARA CATEGORIA
--
CREATE TABLE `category` (
  `ID` int(11) NOT NULL,
  `NAME` text NOT NULL,
  `DESCRIPTION` text DEFAULT NULL,
  `DATE` datetime NOT NULL DEFAULT current_timestamp()
);

ALTER TABLE `category`
  ADD PRIMARY KEY (`ID`);

ALTER TABLE `category`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;


--
-- CRIAR TABELA PARA PRODUTO
--
CREATE TABLE `product` (
  `ID` int(11) NOT NULL,
  `NAME` text NOT NULL,
  `DESCRIPTION` text DEFAULT NULL,
  `PRICE` decimal(30,2) NOT NULL DEFAULT 0.00,
  `ID_CATEGORY` int(11) NOT NULL,
  `DATE` date NOT NULL DEFAULT current_timestamp(),
  `URLIMAGE` text DEFAULT NULL
);

ALTER TABLE `product`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `id_fk_category` (`ID_CATEGORY`);

ALTER TABLE `product`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

ALTER TABLE `product`
  ADD CONSTRAINT `id_fk_category` FOREIGN KEY (`ID_CATEGORY`) REFERENCES `category` (`ID`);


--
-- INSERE ALGUNS VALORES PARA TESTE
--
INSERT INTO `category` (`NAME`, `DESCRIPTION`) VALUES
('Categoria', 'Primeira categoria'),
('Categoria 2', 'Categoria não utilizada');

INSERT INTO `product` (`NAME`, `DESCRIPTION`, `PRICE`, `ID_CATEGORY`, `URLIMAGE`) VALUES
('Produto', 'Primeiro produto', '12.55', 1, 'https://upload.wikimedia.org/wikipedia/commons/thumb/8/8e/Nails.jpg/220px-Nails.jpg'),
('Tábua de Carne', 'Nova tábua muito boa', '14.99', 1, 'https://upload.wikimedia.org/wikipedia/commons/thumb/7/74/Chopping_Board.jpg/200px-Chopping_Board.jpg');