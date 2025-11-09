# Application Load Balancer (ALB) Module
This Terraform module creates an Application Load Balancer (ALB) with target groups for React and Angular web UI applications, and configures path-based routing.

## Requirements

| Name | Version |
| :-- | :-- |
| terraform | >= 0.13.1 |
| aws | >= 3.0 |

## Resources

| Name | Type |
| :-- | :-- |
| aws_lb.this | resource |
| aws_lb_target_group.web_ui_react | resource |
| aws_lb_target_group.web_ui_angular | resource |
| aws_lb_listener.http | resource |
| aws_lb_listener_rule.angular_path | resource |

## Inputs

| Name | Description | Type | Default | Required |
| :-- | :-- | :-- | :-- | :-- |
| name | Name for the load balancer | string | "lb" | no |
| vpc_id | ID of the VPC where the load balancer will be deployed | string | n/a | yes |
| subnet_ids | List of subnet IDs for the load balancer | list(string) | n/a | yes |
| security_group_id | Security group ID for the load balancer | string | n/a | yes |
| web_backend_port | Port for Web Backend service | number | n/a | yes |
| web_ui_port | Port for Web UI service | number | n/a | yes |
| tags | A map of tags to add to all resources | map(string) | {} | no |

## Outputs

| Name | Description |
| :-- | :-- |
| lb_id | The ID of the load balancer |
| lb_arn | The ARN of the load balancer |
| lb_dns_name | The DNS name of the load balancer |
| lb_zone_id | The canonical hosted zone ID of the load balancer |
| web_ui_angular_target_group_arn | ARN of the Angular web UI target group |
| web_ui_react_target_group_arn | ARN of the Web UI target group |
| alb_dns_name | The DNS name of the load balancer |