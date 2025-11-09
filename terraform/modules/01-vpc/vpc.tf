locals {
  public_subnets  = { for k, v in var.subnets : k => v if v.public }
  private_subnets = { for k, v in var.subnets : k => v if !v.public }
}


data "aws_availability_zones" "available" {
  state = "available"
}

################################################################################
# VPC
################################################################################

resource "aws_vpc" "vpc" {
  cidr_block = var.vpc_cidr
  tags = merge(
    { "Name" = var.vpc_name },
    var.tags
  )
}

################################################################################
# Subnets
################################################################################

resource "aws_subnet" "this" {
  for_each = var.subnets

  vpc_id                  = aws_vpc.vpc.id
  availability_zone       = data.aws_availability_zones.available.names[try(coalesce(each.value.az_index, 0), 0)]
  cidr_block              = each.value.cidr_block
  map_public_ip_on_launch = each.value.public

  tags = {
    Name = each.key
    Type = each.value.public ? "public" : "private"
  }
}

################################################################################
# Internet Gateway
################################################################################

resource "aws_internet_gateway" "this" {
  count = contains([for s in values(var.subnets) : s.public], true) ? 1 : 0

  vpc_id = aws_vpc.vpc.id
  tags = merge(
    { "Name" = "vpc-igw" },
    var.tags
  )
}

################################################################################
# Route Tables
################################################################################

resource "aws_route_table" "public" {
  count  = length(local.public_subnets) == 0 ? 0 : 1
  vpc_id = aws_vpc.vpc.id

  tags = { Name = "public-rt" }
}

resource "aws_route" "public_internet" {
  count = length(local.public_subnets) == 0 ? 0 : 1

  route_table_id         = aws_route_table.public[0].id
  destination_cidr_block = "0.0.0.0/0"
  gateway_id             = aws_internet_gateway.this[0].id
}

resource "aws_route_table" "private" {
  count  = length(local.private_subnets) == 0 ? 0 : 1
  vpc_id = aws_vpc.vpc.id
  tags   = { Name = "private-rt" }
}

################################################################################
# Route Tables associations
################################################################################

resource "aws_route_table_association" "public" {
  for_each = local.public_subnets

  subnet_id      = aws_subnet.this[each.key].id
  route_table_id = aws_route_table.public[0].id
}

resource "aws_route_table_association" "private" {
  for_each = local.private_subnets

  subnet_id      = aws_subnet.this[each.key].id
  route_table_id = aws_route_table.private[0].id
}
