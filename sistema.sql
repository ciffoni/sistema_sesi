-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Tempo de geração: 30-Jun-2025 às 22:31
-- Versão do servidor: 10.4.24-MariaDB
-- versão do PHP: 8.1.6

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Banco de dados: `sistema`
--
CREATE DATABASE IF NOT EXISTS `sistema` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `sistema`;

-- --------------------------------------------------------

--
-- Estrutura da tabela `itenspedido`
--

DROP TABLE IF EXISTS `itenspedido`;
CREATE TABLE `itenspedido` (
  `iditens` int(11) NOT NULL,
  `idproduto` int(11) DEFAULT NULL,
  `quantidade` int(11) DEFAULT NULL,
  `total` decimal(5,2) DEFAULT NULL,
  `idpedido` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Extraindo dados da tabela `itenspedido`
--

INSERT INTO `itenspedido` (`iditens`, `idproduto`, `quantidade`, `total`, `idpedido`) VALUES
(1, 14, 1, '12.90', 0),
(2, 16, 1, '20.50', 0),
(3, 17, 1, '8.50', 0),
(4, 15, 1, '999.99', 6),
(5, 17, 1, '8.50', 6),
(6, 14, 1, '12.90', 7),
(7, 17, 1, '8.50', 7),
(8, 18, 2, '17.00', 8),
(9, 19, 1, '8.50', 9),
(10, 18, 2, '17.00', 9),
(11, 20, 1, '15.00', 10);

--
-- Acionadores `itenspedido`
--
DROP TRIGGER IF EXISTS `trg_baixa_estoque_apos_venda`;
DELIMITER $$
CREATE TRIGGER `trg_baixa_estoque_apos_venda` AFTER INSERT ON `itenspedido` FOR EACH ROW BEGIN
    -- Atualiza a quantidade em estoque na tabela 'produto'
    -- A nova quantidade será a quantidade atual menos a quantidade vendida (NEW.Quantidade)
    UPDATE produto
    SET produto.quantidade = produto.quantidade - NEW.Quantidade
    WHERE produto.id = NEW.idproduto;

    -- Opcional: Adicionar uma verificação para estoque negativo (se necessário)
    -- Se você quiser evitar que o estoque fique negativo, ou registrar um aviso,
    -- pode adicionar lógica aqui. Por exemplo:
    -- IF (SELECT quantidade FROM produto WHERE id = NEW.ProdutoId) < 0 THEN
    --     -- Sinalize um erro ou insira um registro em uma tabela de logs de estoque negativo
    --     -- SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Estoque insuficiente para o produto!';
    -- END IF;
END
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Estrutura da tabela `pedido`
--

DROP TABLE IF EXISTS `pedido`;
CREATE TABLE `pedido` (
  `idpedido` int(11) NOT NULL,
  `idusuario` int(11) DEFAULT NULL,
  `formapagamento` varchar(60) DEFAULT NULL,
  `data_pedido` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `status` varchar(60) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Extraindo dados da tabela `pedido`
--

INSERT INTO `pedido` (`idpedido`, `idusuario`, `formapagamento`, `data_pedido`, `status`) VALUES
(3, 1, 'PIX', '2025-06-24 17:34:45', 'andamento'),
(4, 1, 'dinheiro', '2025-06-24 17:42:31', 'andamento'),
(5, 1, 'Cartão credito', '2025-06-24 17:49:32', 'concluido'),
(6, 2, 'Cartão credito', '2025-06-24 17:53:55', 'cancelado'),
(7, 1, 'dinheiro', '2025-06-24 19:53:50', 'andamento'),
(8, 1, 'PIX', '2025-06-30 19:43:51', 'concluido'),
(9, 1, 'Cartão credito', '2025-06-30 20:27:23', 'concluido'),
(10, 1, 'dinheiro', '2025-06-30 20:29:09', 'concluido');

-- --------------------------------------------------------

--
-- Estrutura da tabela `produto`
--

DROP TABLE IF EXISTS `produto`;
CREATE TABLE `produto` (
  `id` int(11) NOT NULL,
  `descricao` varchar(100) DEFAULT NULL,
  `quantidade` int(11) DEFAULT NULL,
  `preco` decimal(10,2) DEFAULT NULL,
  `datacadastro` date DEFAULT NULL,
  `foto` varchar(160) DEFAULT NULL,
  `promocao` tinyint(4) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Extraindo dados da tabela `produto`
--

INSERT INTO `produto` (`id`, `descricao`, `quantidade`, `preco`, `datacadastro`, `foto`, `promocao`) VALUES
(18, 'café expresso', 18, '8.50', '2025-06-24', 'ImagensProdutos\\cafeexpresspo.png', 1),
(19, 'coxinha de frango', 24, '8.50', '2025-06-24', 'ImagensProdutos\\coxinha frango catupiry.png', 1),
(20, 'bolo prestigio', 7, '15.00', '2025-06-24', 'ImagensProdutos\\Bolo-recheado-de-prestigio.jpg', 1),
(21, 'mesa', 2, '2500.00', '2025-06-24', 'ImagensProdutos\\38170080-mesa-de-jantar-6-lugares-com-tampo-de-vidro-dafne-plus-moveis-lopas-7898453648619-2_zoom-1500x1500.jpg', 0);

-- --------------------------------------------------------

--
-- Estrutura da tabela `usuario`
--

DROP TABLE IF EXISTS `usuario`;
CREATE TABLE `usuario` (
  `id` int(11) NOT NULL,
  `nome` varchar(60) DEFAULT NULL,
  `email` varchar(60) DEFAULT NULL,
  `senha` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Extraindo dados da tabela `usuario`
--

INSERT INTO `usuario` (`id`, `nome`, `email`, `senha`) VALUES
(1, 'jorge ciffoni', 'ciffoni@gmail.com', 'aula123'),
(2, 'balcao', 'balcao@gmail.com', 'balcao123');

--
-- Índices para tabelas despejadas
--

--
-- Índices para tabela `itenspedido`
--
ALTER TABLE `itenspedido`
  ADD PRIMARY KEY (`iditens`);

--
-- Índices para tabela `pedido`
--
ALTER TABLE `pedido`
  ADD PRIMARY KEY (`idpedido`);

--
-- Índices para tabela `produto`
--
ALTER TABLE `produto`
  ADD PRIMARY KEY (`id`);

--
-- Índices para tabela `usuario`
--
ALTER TABLE `usuario`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT de tabelas despejadas
--

--
-- AUTO_INCREMENT de tabela `itenspedido`
--
ALTER TABLE `itenspedido`
  MODIFY `iditens` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT de tabela `pedido`
--
ALTER TABLE `pedido`
  MODIFY `idpedido` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT de tabela `produto`
--
ALTER TABLE `produto`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

--
-- AUTO_INCREMENT de tabela `usuario`
--
ALTER TABLE `usuario`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
