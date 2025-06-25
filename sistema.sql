-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Tempo de geração: 25/06/2025 às 13:09
-- Versão do servidor: 10.4.32-MariaDB
-- Versão do PHP: 8.1.25

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
create database sistema;
use sistema;
-- --------------------------------------------------------

--
-- Estrutura para tabela `itenspedido`
--

CREATE TABLE `itenspedido` (
  `iditens` int(11) NOT NULL,
  `idproduto` int(11) DEFAULT NULL,
  `quantidade` int(11) DEFAULT NULL,
  `total` decimal(5,2) DEFAULT NULL,
  `idpedido` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `itenspedido`
--

INSERT INTO `itenspedido` (`iditens`, `idproduto`, `quantidade`, `total`, `idpedido`) VALUES
(1, 14, 1, 12.90, 0),
(2, 16, 1, 20.50, 0),
(3, 17, 1, 8.50, 0),
(4, 15, 1, 999.99, 6),
(5, 17, 1, 8.50, 6),
(6, 14, 1, 12.90, 7),
(7, 17, 1, 8.50, 7);

-- --------------------------------------------------------

--
-- Estrutura para tabela `pedido`
--

CREATE TABLE `pedido` (
  `idpedido` int(11) NOT NULL,
  `idusuario` int(11) DEFAULT NULL,
  `formapagamento` varchar(60) DEFAULT NULL,
  `data_pedido` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `status` varchar(60) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `pedido`
--

INSERT INTO `pedido` (`idpedido`, `idusuario`, `formapagamento`, `data_pedido`, `status`) VALUES
(3, 1, 'PIX', '2025-06-24 17:34:45', 'andamento'),
(4, 1, 'dinheiro', '2025-06-24 17:42:31', 'andamento'),
(5, 1, 'Cartão credito', '2025-06-24 17:49:32', 'concluido'),
(6, 2, 'Cartão credito', '2025-06-24 17:53:55', 'cancelado'),
(7, 1, 'dinheiro', '2025-06-24 19:53:50', 'andamento');

-- --------------------------------------------------------

--
-- Estrutura para tabela `produto`
--

CREATE TABLE `produto` (
  `id` int(11) NOT NULL,
  `descricao` varchar(100) DEFAULT NULL,
  `quantidade` int(11) DEFAULT NULL,
  `preco` decimal(10,2) DEFAULT NULL,
  `datacadastro` date DEFAULT NULL,
  `foto` varchar(160) DEFAULT NULL,
  `promocao` tinyint(4) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `produto`
--

INSERT INTO `produto` (`id`, `descricao`, `quantidade`, `preco`, `datacadastro`, `foto`, `promocao`) VALUES
(14, 'batata frita sadia', 10, 12.90, '2025-06-25', 'C:\\Users\\pcbr4\\OneDrive\\Favoritos\\Documentos\\GitHub\\sistema_sesi\\fotos\\batata frita.png', 1),
(15, 'pizza sadia calabresa', 30, 1850.00, '2025-06-24', 'C:\\Users\\pcbr4\\OneDrive\\Favoritos\\Documentos\\GitHub\\sistema_sesi\\fotos\\pizza.png', 0),
(16, 'prato feito', 35, 20.50, '2025-06-25', 'C:\\Users\\pcbr4\\OneDrive\\Favoritos\\Documentos\\GitHub\\sistema_sesi\\fotos\\prato feito.png', 1),
(17, 'coca cola', 50, 8.50, '2025-06-24', 'C:\\Users\\pcbr4\\OneDrive\\Favoritos\\Documentos\\GitHub\\sistema_sesi\\fotos\\coca cola.png', 0);

-- --------------------------------------------------------

--
-- Estrutura para tabela `usuario`
--

CREATE TABLE `usuario` (
  `id` int(11) NOT NULL,
  `nome` varchar(60) DEFAULT NULL,
  `email` varchar(60) DEFAULT NULL,
  `senha` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `usuario`
--

INSERT INTO `usuario` (`id`, `nome`, `email`, `senha`) VALUES
(1, 'jorge ciffoni', 'ciffoni@gmail.com', 'aula123'),
(2, 'balcao', 'balcao@gmail.com', 'balcao123');

--
-- Índices para tabelas despejadas
--
select * from produto;
--
-- Índices de tabela `itenspedido`
--
ALTER TABLE `itenspedido`
  ADD PRIMARY KEY (`iditens`);

--
-- Índices de tabela `pedido`
--
ALTER TABLE `pedido`
  ADD PRIMARY KEY (`idpedido`);

--
-- Índices de tabela `produto`
--
ALTER TABLE `produto`
  ADD PRIMARY KEY (`id`);

--
-- Índices de tabela `usuario`
--
ALTER TABLE `usuario`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT para tabelas despejadas
--

--
-- AUTO_INCREMENT de tabela `itenspedido`
--
ALTER TABLE `itenspedido`
  MODIFY `iditens` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT de tabela `pedido`
--
ALTER TABLE `pedido`
  MODIFY `idpedido` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT de tabela `produto`
--
ALTER TABLE `produto`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- AUTO_INCREMENT de tabela `usuario`
--
ALTER TABLE `usuario`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;


select * from pedido;
select * from itenspedido;