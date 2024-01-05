/*
 Navicat MySQL Data Transfer

 Source Server         : 10.175.5.59_0
 Source Server Type    : MySQL
 Source Server Version : 80030
 Source Host           : 10.175.5.59:0
 Source Schema         : zxltest

 Target Server Type    : MySQL
 Target Server Version : 80030
 File Encoding         : 65001

 Date: 05/01/2024 08:19:20
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for network_history
-- ----------------------------
DROP TABLE IF EXISTS `network_history`;
CREATE TABLE `network_history`  (
  `computer` varchar(100) CHARACTER SET utf8mb3 COLLATE utf8mb3_vietnamese_ci NULL DEFAULT NULL,
  `status1` varchar(10) CHARACTER SET utf8mb3 COLLATE utf8mb3_vietnamese_ci NULL DEFAULT NULL,
  `time_change` datetime(0) NULL DEFAULT NULL,
  `model` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_vietnamese_ci NULL DEFAULT NULL
) ENGINE = InnoDB CHARACTER SET = utf8mb3 COLLATE = utf8mb3_vietnamese_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
