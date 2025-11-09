################################################################################
# ALB Security Group
################################################################################

resource "aws_security_group" "alb" {
  name        = "${var.name_prefix}-alb-sg"
  description = "Security group for Application Load Balancer"
  vpc_id      = var.vpc_id

  tags = merge(
    { Name = "${var.name_prefix}-alb-sg" },
    var.tags
  )
}

resource "aws_vpc_security_group_ingress_rule" "alb_ports" {
  for_each = { for idx, port in var.alb_ingress_ports : tostring(port) => port }

  security_group_id = aws_security_group.alb.id
  cidr_ipv4         = var.alb_ingress_cidr[0]
  from_port         = each.value
  to_port           = each.value
  ip_protocol       = "tcp"
  description       = "Allow traffic on port ${each.value}"
}

resource "aws_vpc_security_group_egress_rule" "alb_egress" {
  security_group_id = aws_security_group.alb.id

  cidr_ipv4   = "0.0.0.0/0"
  ip_protocol = "-1" # All protocols
  description = "Allow all outbound traffic"
}

################################################################################
# Web Backend Security Group
################################################################################

resource "aws_security_group" "web_backend" {
  name        = "${var.name_prefix}-web-backend-sg"
  description = "Security group for Web Backend instances"
  vpc_id      = var.vpc_id

  tags = merge(
    { Name = "${var.name_prefix}-web-backend-sg" },
    var.tags
  )
}

resource "aws_vpc_security_group_ingress_rule" "alb_to_backend" {
  security_group_id            = aws_security_group.web_backend.id
  referenced_security_group_id = aws_security_group.alb.id
  from_port                    = var.web_backend_port
  to_port                      = var.web_backend_port
  ip_protocol                  = "tcp"
  description                  = "Allow ALB to forward traffic to Backend on port 8080"
}

resource "aws_vpc_security_group_ingress_rule" "web_backend_from_web_ui" {
  security_group_id = aws_security_group.web_backend.id

  referenced_security_group_id = aws_security_group.web_ui.id
  from_port                    = var.web_backend_port
  to_port                      = var.web_backend_port
  ip_protocol                  = "tcp"
  description                  = "Allow API requests from Web UI to Web Backend"
}

resource "aws_vpc_security_group_egress_rule" "web_backend_egress" {
  security_group_id = aws_security_group.web_backend.id

  cidr_ipv4   = "0.0.0.0/0"
  ip_protocol = "-1" # All protocols
  description = "Allow all outbound traffic"
}

################################################################################
# Web UI Security Group
################################################################################

resource "aws_security_group" "web_ui" {
  name        = "${var.name_prefix}-web-ui-sg"
  description = "Security group for Web UI instances"
  vpc_id      = var.vpc_id

  tags = merge(
    { Name = "${var.name_prefix}-web-ui-sg" },
    var.tags
  )
}

resource "aws_vpc_security_group_ingress_rule" "web_ui_from_alb" {
  security_group_id = aws_security_group.web_ui.id

  referenced_security_group_id = aws_security_group.alb.id
  from_port                    = var.web_ui_port
  to_port                      = var.web_ui_port
  ip_protocol                  = "tcp"
  description                  = "Allow traffic from ALB to Web UI"
}

resource "aws_vpc_security_group_ingress_rule" "web_ui_http_public" {
  security_group_id = aws_security_group.web_ui.id

  cidr_ipv4   = "0.0.0.0/0"
  from_port   = var.web_ui_port
  to_port     = var.web_ui_port
  ip_protocol = "tcp"
  description = "Allow HTTP from anywhere"
}

resource "aws_vpc_security_group_egress_rule" "web_ui_egress" {
  security_group_id = aws_security_group.web_ui.id

  cidr_ipv4   = "0.0.0.0/0"
  ip_protocol = "-1" # All protocols
  description = "Allow all outbound traffic"
}

################################################################################
# Security Group for RDS
################################################################################

resource "aws_security_group" "rds" {
  description = "Security group for RDS instances"
  name        = "${var.name_prefix}-rds-sg"
  vpc_id      = var.vpc_id

  tags = merge(
    { Name = "${var.name_prefix}-rds-sg" },
    var.tags
  )
}

resource "aws_vpc_security_group_ingress_rule" "rds_from_web_backend" {
  description                  = "Allow access from Web Backend instances"
  security_group_id            = aws_security_group.rds.id
  referenced_security_group_id = aws_security_group.web_backend.id
  from_port                    = var.rds_port
  to_port                      = var.rds_port
  ip_protocol                  = "tcp"
}

resource "aws_vpc_security_group_egress_rule" "rds_block_all_outbound" {
  description       = "Block all outbound traffic"
  security_group_id = aws_security_group.rds.id
  cidr_ipv4         = "0.0.0.0/0"
  ip_protocol       = "-1"
}