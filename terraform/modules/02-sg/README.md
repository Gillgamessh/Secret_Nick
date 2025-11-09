# Security Groups Module
This Terraform module creates security groups for a multi-tier web application architecture, including ALB (Application Load Balancer), Web UI, Web Backend, and RDS (Relational Database Service) components.

## Requirements

| Name | Version |
| :-- | :-- |
| terraform | >= 0.13.1 |
| aws | >= 3.29 |

## Resources

| Name | Type |
|------|------|
| [aws_security_group.alb](https://registry.terraform.io/providers/hashicorp/aws/latest/docs/resources/security_group) | resource |
| [aws_vpc_security_group_ingress_rule](https://registry.terraform.io/providers/hashicorp/aws/latest/docs/resources/vpc_security_group_ingress_rule) | resource |
| [aws_vpc_security_group_egress_rule](https://registry.terraform.io/providers/hashicorp/aws/latest/docs/resources/vpc_security_group_egress_rule) | resource |

## Inputs

| Name | Description | Type | Default | Required |
|------|-------------|------|---------|:--------:|
| <a name="vpc_id"></a> [vpc\_id](#input\_vpc\_id) | VPC name | `string` | n/a | no |
| <a name="name_prefix"></a> [vpc\_cidr](#input\_vpc\_cidr) | Prefix to be used in the name of the security groups  | `string` | n/a | yes |
| <a name="web_backend_port"></a> [web\_backend\_port](#input\_web\_backend\_port) | Port for the web backend service | `number` | 8080 | yes |
| <a name="web_ui_port"></a> [web\_ui\_port](#input\_web\_backend\_port) | Port for the web UI service | `number` | 3000 | yes |
| <a name="rds_port"></a> [rds\_port](#input\_rds\_port) | Port for the RDS service | `number` | 5432 | yes |
| <a name="alb_ingress_ports"></a> [alb\_ingress\_ports](#input\_alb\_ingress\_ports) | List of ports to allow ingress traffic to ALB | `list(number)` | [80] | yes |
| <a name="alb_ingress_cidr"></a> [alb\_ingress\_cidr](#input\_alb\_ingress\_cidr) | CIDR blocks to allow ingress traffic to ALB | `list(string)` | ["0.0.0.0/0"] | yes |

## Outputs

| Name | Description |
|------|-------------|
| <a name="output_alb_security_group_id"></a> [alb\_security\_group\_id](#output\_alb\_security\_group\_id) | ID of the ALB security group |
| <a name="output_web_backend_security_group_id"></a> [web\_backend\_security\_group\_id](#output\_web\_backend\_security\_group\_id) | ID of the Web Backend security group |
| <a name="output_web_ui_security_group_id"></a> [web\_ui\_security\_group\_id](#output\_web\_ui\_security\_group\_id) | ID of the Web UI security group |
| <a name="output_rds_instance_security_group_id"></a> [rds_instance\_security\_group\_id](#output\_rds_instance\_security\_group\_id) | ID of the RDS instance security group |
| <a name="output_rds_security_group_id"></a> [rds\_security\_group\_id](#output\_rds\_security\_group\_id) | ID of the RDS security group |
| <a name="security_groups"></a> [security\_groups](#output\_security\_groups) | Map of all security groups |

## Usage
```
module "security_groups" {
  source = "./modules/02-sg"

  vpc_id      = module.vpc.vpc_id
  name_prefix = var.project_name

  # Port settings
  web_backend_port = var.web_backend_port
  web_ui_port      = var.web_ui_port
  rds_port         = var.rds_port

  # Access settings
  alb_ingress_ports = var.alb_ingress_ports
  alb_ingress_cidr  = var.alb_ingress_cidr

  tags = var.tags
}
```

### Security Group Flow
This module creates the following security groups with specific rules:

ALB Security Group:

Allows inbound traffic on specified ports (default: 80) from specified CIDR blocks (default: 0.0.0.0/0)
Allows all outbound traffic
Web UI Security Group:

Allows inbound traffic on web_ui_port (default: 3000) from ALB security group
Allows all outbound traffic
Web Backend Security Group:

Allows inbound traffic on web_backend_port (default: 8080) from Web UI security group
Allows all outbound traffic
RDS Security Group:

Allows inbound traffic on rds_port from Web Backend security group
Blocks all outbound traffic