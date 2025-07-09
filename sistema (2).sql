-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Tempo de geração: 09/07/2025 às 14:47
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
CREATE DATABASE IF NOT EXISTS `sistema` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `sistema`;

-- --------------------------------------------------------

--
-- Estrutura para tabela `itenspedido`
--

DROP TABLE IF EXISTS `itenspedido`;
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
(54, 18, 1, 8.50, 26),
(55, 20, 1, 35.70, 26);

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
-- Estrutura para tabela `lancamentosfinanceiros`
--

DROP TABLE IF EXISTS `lancamentosfinanceiros`;
CREATE TABLE `lancamentosfinanceiros` (
  `Id` int(11) NOT NULL,
  `DataLancamento` datetime NOT NULL,
  `Descricao` varchar(255) NOT NULL,
  `Valor` decimal(10,2) NOT NULL,
  `Tipo` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `lancamentosfinanceiros`
--

INSERT INTO `lancamentosfinanceiros` (`Id`, `DataLancamento`, `Descricao`, `Valor`, `Tipo`) VALUES
(1, '2025-07-01 10:00:00', 'Compra de matéria-prima', 150.75, 'Saida'),
(2, '2025-07-01 15:30:00', 'Aluguel do mês', 2000.00, 'Saida'),
(3, '2025-07-02 09:00:00', 'Reembolso de cliente', 50.00, 'Entrada');

-- --------------------------------------------------------

--
-- Estrutura para tabela `pedido`
--

DROP TABLE IF EXISTS `pedido`;
CREATE TABLE `pedido` (
  `idpedido` int(11) NOT NULL,
  `idusuario` int(11) DEFAULT NULL,
  `formapagamento` varchar(60) DEFAULT NULL,
  `data_pedido` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `status` varchar(60) NOT NULL,
  `total` decimal(5,2) NOT NULL,
  `tipo` varchar(60) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `pedido`
--

INSERT INTO `pedido` (`idpedido`, `idusuario`, `formapagamento`, `data_pedido`, `status`, `total`, `tipo`) VALUES
(26, 2, 'dinheiro', '2025-07-08 17:41:43', 'Em Preparo', 44.20, 'entrada');

-- --------------------------------------------------------

--
-- Estrutura para tabela `produto`
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `produto`
--

INSERT INTO `produto` (`id`, `descricao`, `quantidade`, `preco`, `datacadastro`, `foto`, `promocao`) VALUES
(18, 'coca cola', 47, 8.50, '2025-06-24', 'ImagensProdutos\\coca cola.png', 1),
(19, 'batata frita', 18, 12.50, '2025-06-25', 'ImagensProdutos\\batata frita.png', 0),
(20, 'prato feito brasileiro', 18, 35.70, '2025-06-24', 'ImagensProdutos\\prato feito.png', 1),
(21, 'pizza calabresa', 35, 18.50, '2025-06-26', 'ImagensProdutos\\pizza.png', 1),
(22, 'coxinha de frango catupiry', 12, 8.50, '2025-06-26', 'ImagensProdutos\\coxinha frango catupiry.png', 1),
(23, 'café expresso', 15, 4.50, '2025-06-24', 'ImagensProdutos\\cafeexpresspo.png', 0),
(24, 'x-salada', 10, 12.00, '2025-07-01', 'ImagensProdutos\\xsalada.jfif', 1);

-- --------------------------------------------------------

--
-- Estrutura para tabela `usuario`
--

DROP TABLE IF EXISTS `usuario`;
CREATE TABLE `usuario` (
  `id` int(11) NOT NULL,
  `nome` varchar(60) DEFAULT NULL,
  `email` varchar(60) DEFAULT NULL,
  `senha` varchar(255) DEFAULT NULL,
  `cargo` varchar(60) NOT NULL,
  `Ativo` tinyint(1) DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `usuario`
--

INSERT INTO `usuario` (`id`, `nome`, `email`, `senha`, `cargo`, `Ativo`) VALUES
(1, 'jorge', 'jorge@gmail.com', 'aula123', 'Gerente', 1),
(2, 'ciffoni', 'ciffoni@gmail.com', '$2b$10$UmZfwrqJu19j16aNHrQfOOLn8sW/M/y6ZX49qVvVmP5cv5z23RknC', 'Gerente', 1),
(3, 'amanda', 'amanda@gmail.com', '$2b$10$QAP8MKr.gj.f0I4OsoVB7eyvRxNLgsO7mPYPU1YUv0ggJ/yZdEGB6', 'Cozinha', 1);

--
-- Índices para tabelas despejadas
--

--
-- Índices de tabela `itenspedido`
--
ALTER TABLE `itenspedido`
  ADD PRIMARY KEY (`iditens`);

--
-- Índices de tabela `lancamentosfinanceiros`
--
ALTER TABLE `lancamentosfinanceiros`
  ADD PRIMARY KEY (`Id`);

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
  MODIFY `iditens` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=56;

--
-- AUTO_INCREMENT de tabela `lancamentosfinanceiros`
--
ALTER TABLE `lancamentosfinanceiros`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de tabela `pedido`
--
ALTER TABLE `pedido`
  MODIFY `idpedido` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=27;

--
-- AUTO_INCREMENT de tabela `produto`
--
ALTER TABLE `produto`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;

--
-- AUTO_INCREMENT de tabela `usuario`
--
ALTER TABLE `usuario`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
